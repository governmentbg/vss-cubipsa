using Legalacts.Model.Entities;
using Legalacts.Utils;
using NLog;
using System;
using System.Linq;
using System.Threading;
using System.Data.Entity;
using System.Collections.Generic;
using Legalacts.Utils.XmlSchemaValidator;
using System.Xml;
using Legalacts.Utils.DocumentSerializer;
using ecli;
using Legalacts.Utils.Managers;
using Legalacts.Model.Sitemap.Entities;
using System.Text;
using System.Data.Entity.Validation;

namespace Legalacts.ECLI.Integrator.Jobs
{
    public class IndexingJob : IJob
    {
        private readonly int READ_ACTS_COUNT = Statics.IndexingJobBatchCount;
        private Logger logger = LogManager.GetLogger("INTEGRATOR_LOGGER");
        private readonly Timer timer;
        private readonly JobHost jobHost;
        private IDocumentSerializer documentSerializer;

        public IndexingJob()
        {
            this.timer = new Timer(this.OnTimerElapsed);
            this.jobHost = new JobHost();
            this.documentSerializer = new DocumentSerializer();
        }

        public void Start()
        {
            this.timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(Statics.IndexingJobIntervalInSeconds));
        }

        public void Dispose()
        {
            this.timer.Dispose();
        }

        private void OnTimerElapsed(object sender)
        {
            this.jobHost.DoAction(() =>
            {
                if (this.jobHost.IsShuttingDown)
                    return;

                try
                {
                    using (LegalactsContext legalact = new LegalactsContext())
                    {
                        legalact.Configuration.ProxyCreationEnabled = false;
                        legalact.Configuration.LazyLoadingEnabled = false;

                        using (SitemapContext sitemap = new SitemapContext())
                        {
                            sitemap.Configuration.ProxyCreationEnabled = false;
                            sitemap.Configuration.LazyLoadingEnabled = false;

                            // read next
                            var newActs = GetNewActs(legalact, sitemap);

                            // read deleted
                            var deletedActs = GetDeletedActs(legalact);

                            // merge new with deleted

                            var mergedActs = newActs.Concat(deletedActs);

                            if (mergedActs.Count() > 0)
                            {
                                // group by date
                                var acts = mergedActs.GroupBy(e => e.ModifyDate.Value.Date)
                                                    .ToDictionary(g => g.Key, g => g.Select(a => new ActWrapper() { Act = a }).OrderByDescending(o => o.Act.ModifyDate).DistinctBy(i => i.Act.ModifyDate)
                                                    .ToList());

                                // convert to ecli doc
                                GenerateEcliDocs(acts);

                                // serialize to ecli xml
                                SerializeEcliDocs(acts, documentSerializer);

                                // find relation and create or update
                                CreateSitemaps(sitemap, acts, newActs.Count() > 0 ? newActs.Max(e => e.ActId) : new Nullable<int>(), documentSerializer);

                                MarkDeletedActsAsSynced(legalact, deletedActs);
                            }
                        }
                    }
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        var error = Environment.NewLine + Environment.NewLine + "!!!!!!!!!!!!!!!"
                            + Helper.CreateExceptionString(e)
                            + $"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:";

                        foreach (var ve in eve.ValidationErrors)
                        {
                            error += $"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"";
                        }

                        logger.Error(error + Environment.NewLine + Environment.NewLine);
                    }

                    throw;
                }
                catch (Exception e)
                {
                    logger.Error(Helper.CreateExceptionString(e) + Environment.NewLine + Environment.NewLine);
                }
            });
        }

        private void MarkDeletedActsAsSynced(LegalactsContext context, List<Act> deletedActs)
        {
            RollBack(context);

            if(deletedActs.Count() > 0)
            {
                var ids = deletedActs.Where(e => e.IsDeleted).Select(e => e.ActId).ToList();

                var dbDeletedActs = context.DeletedActs.Where(e => ids.Contains(e.DeletedActId)).ToList();

                foreach (var da in dbDeletedActs)
                {
                    da.IsSynced = true;
                }

                context.SaveChanges();
            }
        }

        private void RollBack(LegalactsContext context)
        {
            var changedEntries = context.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        private void CreateSitemaps(SitemapContext context, Dictionary<DateTime, List<ActWrapper>> acts, int? lastActId, IDocumentSerializer documentSerializer)
        {
            var availableRoutes = context.Routes
                                    .Include(e => e.Indexes)
                                    .Include(e => e.Indexes.Select(i => i.Sitemaps))
                                    .Where(e => acts.Keys.Contains(e.Date))
                                    .ToList();

            // update

            foreach (var route in availableRoutes)
            {
                var newActs = acts[route.Date];
                var index = route.Indexes.First();
                var sitemap = index.Sitemaps.First();
                var sitemapXml = Decompress(sitemap.XML);

                var sitemapDoc = documentSerializer.XmlDeserializeFromString<sitemap.urlset>(sitemapXml);

                // ***** remove old eclis *******

                var ecliDocs = new List<ecli.document>();
                foreach(var doc in sitemapDoc.urlCollection)
                {
                    var o_doc = documentSerializer.XmlDeserializeFromString<ecli.document>(doc.Any.First().OuterXml);
                    ecliDocs.Add(o_doc);
                }

                foreach (var act in newActs)
                {
                    var idx = ecliDocs.FindIndex(e => e.metadata.isVersionOf.value == act.EcliDocument.metadata.isVersionOf.value);
                    if(idx != -1)
                    {
                        ecliDocs.RemoveAt(idx);
                    }
                }

                var newSitemapDoc = new sitemap.urlset()
                {
                    urlCollection = new sitemap.tUrlCollection()
                };

                foreach (var doc in ecliDocs)
                {
                    newSitemapDoc.urlCollection.Add(new sitemap.tUrl()
                    {
                        loc = doc.metadata.identifierCollection.Last().MixedValue,
                        Any = new XmlElement[1] { GetEcliXmlDocument(doc) }
                    });
                }

                sitemapDoc = newSitemapDoc;

                // ******* end remove *******

                foreach (var act in newActs)
                {
                    var ecliDoc = new sitemap.tUrl()
                    {
                        loc = $"{Statics.LegalactsDomainName}/" + act.Act.EcliCode,
                        Any = new XmlElement[1] { GetEcliXmlDocument(act.EcliDocument) }
                    };

                    sitemapDoc.urlCollection.Add(ecliDoc);
                }

                sitemapXml = documentSerializer.XmlSerializeObjectToString(sitemapDoc);
                sitemap.XML = Compress(sitemapXml);
            }

            // create

            var oldDates = availableRoutes.Select(r => r.Date).ToList();
            var newDates = acts.Keys.Where(e => !oldDates.Contains(e)).ToList();

            foreach (var date in newDates)
            {
                var newActs = acts[date.Date].DistinctBy(e => e.EcliDocument.metadata.isVersionOf.value);

                var sitemap = new Sitemap() { Name = $"{Statics.SITEMAP_KEYWORD}_1" };

                var index = new Index()
                {
                    Name = $"{Statics.SITEMAP_KEYWORD}_{Statics.INDEX_KEYWORD}_1",
                    XML = new byte[1],
                    Sitemaps = new List<Sitemap>() { sitemap }
                };

                var route = new Route()
                {
                    Date = date.Date,
                    Indexes = new List<Index>() { index }
                };

                var sitemapDoc = new sitemap.urlset()
                {
                    urlCollection = new sitemap.tUrlCollection()
                };

                foreach (var act in newActs)
                {
                    sitemapDoc.urlCollection.Add(new sitemap.tUrl()
                    {
                        loc = $"{Statics.LegalactsDomainName}/" + act.Act.EcliCode,
                        Any = new XmlElement[1] { GetEcliXmlDocument(act.EcliDocument) }
                    });
                }

                var indexDoc = new sitemap.sitemapindex()
                {
                    sitemapCollection = new sitemap.tSitemapCollection()
                    {
                        new sitemap.tSitemap()
                        {
                            loc = $"{Statics.DomainName}/{date.Year}/{date.Month.ToString("00")}/{date.Day.ToString("00")}/{sitemap.Name}.xml"
                        }
                    }
                };

                var indexXml = Helper.RemoveXmlDeclarations(documentSerializer.XmlSerializeObjectToString(indexDoc));

                index.XML = Compress(indexXml);

                var sitemapXml = Helper.RemoveXmlDeclarations(documentSerializer.XmlSerializeObjectToString(sitemapDoc));

                sitemap.XML = Compress(sitemapXml);

                context.Set<Route>().Add(route);
            }

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (lastActId.HasValue)
                    {
                        context.SystemVariables.Single(e => e.Key == Statics.LAST_INDEXED_KEY).Value = lastActId.ToString();
                    }

                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    logger.Error(Helper.CreateExceptionString(ex));
                    transaction.Rollback();
                }
            }
        }

        private byte[] Compress(string xml)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(xml);

            return ZipManager.Compress(bytes, Statics.ZIP_KEY);
        }

        private string Decompress(byte[] zip)
        {
            var bytes = ZipManager.Decompress(zip);

            return Encoding.UTF8.GetString(bytes);
        }

        private XmlElement GetEcliXmlDocument(ecli.document document)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(documentSerializer.XmlSerializeToString(document));

            return xmlDoc.DocumentElement;
        }

        private void GenerateEcliDocs(Dictionary<DateTime, List<ActWrapper>> acts)
        {
            foreach (var list in acts.Values)
            {
                foreach (var act in list)
                {
                    var status = act.Act.IsDeleted ? statusType.deleted : statusType.active;
                    act.EcliDocument = ecli.Converter.CreateEcliDocument(act.Act, Statics.LegalactsDomainName, status);
                }
            }
        }

        private void SerializeEcliDocs(Dictionary<DateTime, List<ActWrapper>> acts, IDocumentSerializer serializer)
        {
            foreach (var list in acts.Values)
            {
                foreach (var act in list)
                {
                    // you should validate by schema documents as xml structure
                    act.EcliXml = Helper.RemoveXmlDeclarations(serializer.XmlSerializeToString(act.EcliDocument));
                }
            }
        }

        private List<Act> GetNewActs(LegalactsContext legalacts, SitemapContext sitemap)
        {
            var lastId = int.Parse(sitemap.SystemVariables.Single(e => e.Key == Statics.LAST_INDEXED_KEY).Value);

            var acts = legalacts.Acts
                            .Include(e => e.Court)
                            .Include(e => e.ActKind)
                            .Include(e => e.CaseKind)
                            .Include(e => e.ActDocument)
                            .Include(e => e.MotiveDocument)
                            .Include(e => e.ConnectedActs)
                            .Where(e => e.ActId > lastId)
                            .Where(e => e.EcliCode.Length == 34) // test this
                            .Where(e => Statics.ALLOWED_ACT_KINDS.Contains(e.ActKindId)) // test this
                            .Where(e => DbFunctions.DiffDays(e.CreateDate, DateTime.Now) > 2)
                            .OrderBy(e => e.ActId)
                            .Take(READ_ACTS_COUNT)
                            .ToList();
            
            foreach (var act in acts)
            {
                if (act.ActDocument != null)
                {
                    act.ActDocument.Content = null;
                }

                if (act.MotiveDocument != null)
                {
                    act.MotiveDocument.Content = null;
                }

                //legalacts.Entry(act).State = EntityState.Unchanged;
            }

            return acts;
        }

        private List<Act> GetDeletedActs(LegalactsContext legalacts)
        {
            return legalacts.DeletedActs
                            .Include(e => e.Court)
                            .Include(e => e.ActKind)
                            .Include(e => e.CaseKind)
                            .Where(e => !e.IsSynced)
                            .Where(e => e.EcliCode.Length == 34)
                            .ToList()
                            .Select(e => new Act
                            {
                                ActId = e.DeletedActId,
                                ActNumber = e.ActNumber,
                                CaseNumber = e.CaseNumber,
                                Judge = e.Judge,
                                ActKindId = e.ActKindId,
                                ActKind = e.ActKind,
                                CaseKindId = e.CaseKindId,
                                CaseKind = e.CaseKind,
                                CaseYear = e.CaseYear,
                                ActYear = e.ActYear,
                                CourtId = e.CourtId,
                                Court = e.Court,
                                StartDate = e.StartDate,
                                StatusId = e.StatusId,
                                ResultOfAppeal = e.ResultOfAppeal,
                                UID = e.UID,
                                CreateDate = e.ModifyDate,
                                ModifyDate = e.ModifyDate,
                                EcliCode = e.EcliCode,
                                PreviousEcliCode = e.PreviousEcliCode,
                                IsDeleted = true
                            }).ToList();
        }

        
    }

    class ActWrapper
    {
        public Act Act { get; set; }
        public ecli.document EcliDocument { get; set; }
        public string EcliXml { get; set; }
    }

    static class Extensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}