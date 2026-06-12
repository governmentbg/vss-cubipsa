using Legalacts.Model.Repositories.SitemapRepository;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using Legalacts.Utils.DocumentSerializer;
using Legalacts.Utils.Managers;
using System.Text;
using System.Xml;
using Ninject;
using Legalacts.Utils.XmlSchemaValidator;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Legalacts.Model.Entities;
using System.Data.Entity;
using Legalacts.Model.Sitemap.Entities;
using Legalacts.Utils;

namespace Legalacts.ECLI.Integrator.Controllers
{
    public class TestController : ApiController
    {
        [Inject]
        public ISitemapRepository _sitemapRepository { get; set; }

        [Inject]
        public IXmlSchemaValidator _xmlSchemaValidator { get; set; }

        [HttpGet]
        public HttpResponseMessage Index()
        {
            string result = "Service is running normally! Server time: " + DateTime.Now;

            var response = new HttpResponseMessage(HttpStatusCode.OK);

            response.Content = new StringContent(result, System.Text.Encoding.UTF8, "text/plain");

            return response;
        }

        [HttpGet]
        [Route("check/{sitemapId}")]
        public HttpResponseMessage check(int sitemapId)
        {
            string result = "Start: " + DateTime.Now + Environment.NewLine;
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            result += Environment.NewLine + Environment.NewLine;

            result += CheckDublications(sitemapId);

            result += Environment.NewLine + Environment.NewLine;

            result += "End: " + DateTime.Now;
            response.Content = new StringContent(result, System.Text.Encoding.UTF8, "text/plain");

            return response;
        }

        [HttpGet]
        [Route("remove/{sitemapId}")]
        public HttpResponseMessage update(int sitemapId)
        {
            string result = "Start: " + DateTime.Now + Environment.NewLine;
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            result += Environment.NewLine + Environment.NewLine;

            result += RemoveDublications(sitemapId);

            result += Environment.NewLine + Environment.NewLine;

            result += "End: " + DateTime.Now;
            response.Content = new StringContent(result, System.Text.Encoding.UTF8, "text/plain");
            return response;
        }

        #region Private

        //private string RemovePreviousEcliCodes()
        //{
        //    var sb = new StringBuilder();
        //    var documentSerializer = new DocumentSerializer();

        //    using (var context = new SitemapContext())
        //    {
        //        context.Configuration.LazyLoadingEnabled = false;
        //        context.Configuration.ProxyCreationEnabled = false;
        //        context.Database.CommandTimeout = 180;

        //        var sitemapIds = context.Set<Model.Sitemap.Entities.Sitemap>().Select(e => e.SitemapId).ToList();
        //        sitemapIds.Sort();

        //        var newUpdatedEcliDocs = new List<ecli.document>();

        //        foreach (var id in sitemapIds)
        //        {
        //            var sitemap = context.Set<Model.Sitemap.Entities.Sitemap>().Single(e => e.SitemapId == id);

        //            var sitemapXml = Decompress(sitemap.XML);

        //            var sitemapDoc = documentSerializer.XmlDeserializeFromString<sitemap.urlset>(sitemapXml);

        //            foreach (var ecliXmlE in sitemapDoc.urlCollection)
        //            {
        //                var xml = ecliXmlE.Any.First().OuterXml;
        //                var ecliDoc = documentSerializer.XmlDeserializeFromString<ecli.document>(xml);

        //                if (ecliDoc.metadata.isReplacedBy != null )
        //                {
        //                    if(ecliDoc.status == ecli.statusType.deleted)
        //                    {
        //                        if (newUpdatedEcliDocs.Any(e => e.metadata.isVersionOf.value == ecliDoc.metadata.isVersionOf.value))
        //                        {
        //                            newUpdatedEcliDocs.RemoveAll(e => e.metadata.isVersionOf.value == ecliDoc.metadata.isVersionOf.value);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        ecliDoc.metadata.isReplacedBy = null;
        //                        newUpdatedEcliDocs.Add(ecliDoc);
        //                    }
        //                }
        //            }
        //        }

        //        var routes = new Dictionary<DateTime, List<ecli.document>>();

        //        var startDate = new DateTime(2018, 11, 20);
        //        var endDate = new DateTime(2018, 12, 31);

        //        var nextDays = Enumerable.Range(0, 1 + (endDate.Date.Subtract(startDate).Days))
        //                                  .Select(offset => startDate.AddDays(offset))
        //                                  .ToList();

        //        for (int i = 0; i < newUpdatedEcliDocs.Count(); i++)
        //        {
        //            var mod = i % nextDays.Count();

        //            var date = nextDays[mod];
        //            if (!routes.ContainsKey(date))
        //            {
        //                routes.Add(date, new List<ecli.document>());
        //            }

        //            routes[date].Add(newUpdatedEcliDocs[i]);

        //        }

        //        sb.AppendLine($"Общ брой документ: {newUpdatedEcliDocs.Count().ToString()}");
        //        sb.AppendLine(Environment.NewLine + $"Разпределение по дати:");

        //        foreach (var newRoute in routes)
        //        {
        //            sb.AppendLine($"{newRoute.Key.ToString("dd.MM.yyyy")}г. : {newRoute.Value.Count().ToString()}");
        //        }

        //        foreach (var route in routes)
        //        {
        //            var sitemap = new Sitemap() { Name = $"{Statics.SITEMAP_KEYWORD}_1" };

        //            var index = new Index()
        //            {
        //                Name = $"{Statics.SITEMAP_KEYWORD}_{Statics.INDEX_KEYWORD}_1",
        //                XML = new byte[1],
        //                Sitemaps = new List<Sitemap>() { sitemap }
        //            };

        //            var r = new Route()
        //            {
        //                Date = route.Key,
        //                Indexes = new List<Index>() { index }
        //            };

        //            var sitemapDoc = new sitemap.urlset()
        //            {
        //                urlCollection = new sitemap.tUrlCollection()
        //            };

        //            foreach (var doc in route.Value)
        //            {
        //                sitemapDoc.urlCollection.Add(new sitemap.tUrl()
        //                {
        //                    loc = $"{Statics.LegalactsDomainName}/" + doc.metadata.isVersionOf.value,
        //                    Any = new XmlElement[1] { GetEcliXmlDocument(doc, documentSerializer) }
        //                });
        //            }

        //            var indexDoc = new sitemap.sitemapindex()
        //            {
        //                sitemapCollection = new sitemap.tSitemapCollection()
        //                    {
        //                        new sitemap.tSitemap()
        //                        {
        //                            loc = $"{Statics.DomainName}/{route.Key.Year}/{route.Key.Month.ToString("00")}/{route.Key.Day.ToString("00")}/{sitemap.Name}.xml"
        //                        }
        //                    }
        //            };

        //            var indexXml = Helper.RemoveXmlDeclarations(documentSerializer.XmlSerializeObjectToString(indexDoc));

        //            index.XML = Compress(indexXml);

        //            var sitemapXml = Helper.RemoveXmlDeclarations(documentSerializer.XmlSerializeObjectToString(sitemapDoc));

        //            sitemap.XML = Compress(sitemapXml);

        //            using (var transaction = context.Database.BeginTransaction())
        //            {
        //                try
        //                {
        //                    context.Set<Route>().Add(r);

        //                    context.SaveChanges();
        //                    transaction.Commit();
        //                }
        //                catch (Exception ex)
        //                {
        //                    sb.AppendLine(Helper.CreateExceptionString(ex));
        //                    transaction.Rollback();
        //                }
        //            }
        //        }
        //    }

        //    return sb.ToString();
        //}

        private string RemoveDublications(int minSitemapId)
        {
            var sb = new StringBuilder();
            var documentSerializer = new DocumentSerializer();

            using (var context = new SitemapContext())

            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                context.Database.CommandTimeout = 180;

                var sitemapIds = context.Set<Model.Sitemap.Entities.Sitemap>().Where(e => e.SitemapId >= minSitemapId).Select(e => e.SitemapId).ToList();
                sitemapIds.Sort();

                foreach (var id in sitemapIds)
                {
                    var sitemap = context.Set<Model.Sitemap.Entities.Sitemap>().Single(e => e.SitemapId == id);

                    var sitemapXml = Decompress(sitemap.XML);

                    var sitemapDoc = documentSerializer.XmlDeserializeFromString<sitemap.urlset>(sitemapXml);

                    var ecliDocs = new List<ecli.document>();

                    var save = false;

                    var refCount = sitemapDoc.urlCollection.Count();

                    foreach (var ecliXmlE in sitemapDoc.urlCollection)
                    {
                        var index = 0;
                        var xml = ecliXmlE.Any.First().OuterXml;
                        var ecliDoc = documentSerializer.XmlDeserializeFromString<ecli.document>(xml);

                        index = ecliDocs.FindIndex(e => e.metadata.isVersionOf.value.Trim().ToUpper() == ecliDoc.metadata.isVersionOf.value.Trim().ToUpper());

                        if (index != -1)
                        {
                            ecliDocs[index] = ecliDoc;
                            save = true;
                            sb.AppendLine("sitemapId: " + id.ToString() + " | issued:" + ecliDoc.metadata.issued.Value + " | " + ecliDoc.metadata.isVersionOf.value);
                        }
                        else
                        {
                            ecliDocs.Add(ecliDoc);
                        }
                    }

                    if (save)
                    {
                        sitemapDoc = new sitemap.urlset();

                        foreach (var ecliDoc in ecliDocs)
                        {
                            sitemapDoc.urlCollection.Add(new sitemap.tUrl()
                            {
                                loc = ecliDoc.metadata.identifierCollection.Last().MixedValue,
                                Any = new XmlElement[1] { GetEcliXmlDocument(ecliDoc, documentSerializer) }
                            });
                        }

                        sitemapXml = documentSerializer.XmlSerializeObjectToString(sitemapDoc);
                        sitemap.XML = Compress(sitemapXml);

                        context.SaveChanges();
                    }
                }
            }

            return sb.ToString();
        }

        private string CheckDublications(int minSitemapId)
        {
            var sb = new StringBuilder();
            var documentSerializer = new DocumentSerializer();

            using (var context = new SitemapContext())

            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                context.Database.CommandTimeout = 180;

                var sitemapIds = context.Set<Model.Sitemap.Entities.Sitemap>().Where(e => e.SitemapId >= minSitemapId).Select(e => e.SitemapId).ToList();
                sitemapIds.Sort();

                foreach (var id in sitemapIds)
                {
                    var sitemap = context.Set<Model.Sitemap.Entities.Sitemap>().Single(e => e.SitemapId == id);

                    var sitemapXml = Decompress(sitemap.XML);

                    var sitemapDoc = documentSerializer.XmlDeserializeFromString<sitemap.urlset>(sitemapXml);

                    var ecliDocs = new List<ecli.document>();

                    var refCount = sitemapDoc.urlCollection.Count();

                    foreach (var ecliXmlE in sitemapDoc.urlCollection)
                    {
                        var index = 0;
                        var xml = ecliXmlE.Any.First().OuterXml;
                        var ecliDoc = documentSerializer.XmlDeserializeFromString<ecli.document>(xml);

                        index = ecliDocs.FindIndex(e => e.metadata.isVersionOf.value.Trim().ToUpper() == ecliDoc.metadata.isVersionOf.value.Trim().ToUpper());

                        if (index != -1)
                        {
                            ecliDocs[index] = ecliDoc;
                            sb.AppendLine("sitemapId: " + id.ToString() + " | issued:" + ecliDoc.metadata.issued.Value + " | " + ecliDoc.metadata.isVersionOf.value);
                        }
                        else
                        {
                            ecliDocs.Add(ecliDoc);
                        }
                    }
                }
            }

            return sb.ToString();
        }

        private Dictionary<int, int> GetStats()
        {
            Dictionary<int, int> stats = new Dictionary<int, int>();
            var documentSerializer = new DocumentSerializer();

            using (var context = new SitemapContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                context.Database.CommandTimeout = 180;

                var sitemapIds = context.Set<Model.Sitemap.Entities.Sitemap>()
                    .Select(e => e.SitemapId).ToList();
                sitemapIds.Sort();

                foreach (var id in sitemapIds)
                {
                    var sitemap = context.Set<Model.Sitemap.Entities.Sitemap>()
                        .Include(e => e.Index)
                        .Include(e => e.Index.Route)
                        .Single(e => e.SitemapId == id);

                    var sitemapXml = Decompress(sitemap.XML);

                    var sitemapDoc = documentSerializer.XmlDeserializeFromString<sitemap.urlset>(sitemapXml);

                    var year = sitemap.Index.Route.Date.Year;

                    if (!stats.ContainsKey(year))
                    {
                        stats.Add(year, 0);
                    }

                    stats[year] += sitemapDoc.urlCollection.Count();
                }

                return stats;
            }
        }

        private string SchemaValidation()
        {
            var sb = new StringBuilder();
            var documentSerializer = new DocumentSerializer();


            using (var context = new SitemapContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                context.Configuration.ProxyCreationEnabled = false;
                context.Database.CommandTimeout = 180;

                var sitemaps = context.Set<Sitemap>().ToList();

                foreach (var sitemap in sitemaps)
                {
                    var sitemapXml = Decompress(sitemap.XML);

                    var sitemapDoc = documentSerializer.XmlDeserializeFromString<sitemap.urlset>(sitemapXml);

                    foreach (var ecliXmlE in sitemapDoc.urlCollection)
                    {
                        var xml = ecliXmlE.Any.First().OuterXml;

                        var path = Path.Combine(Directory.GetParent(Path.GetDirectoryName(
                            Assembly.GetExecutingAssembly().EscapedCodeBase.Substring(8))).FullName, "ecliSchemas");

                        var errors = _xmlSchemaValidator.Validate(xml, path);

                        if (errors.Count() > 0)
                        {
                            foreach (var error in errors)
                            {
                                sb.AppendLine(error.Item1);
                            }
                        }
                    }
                }
            }

            return sb.ToString();
        }

        #region Utils

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

        private XmlElement GetEcliXmlDocument(ecli.document document, IDocumentSerializer documentSerializer)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(documentSerializer.XmlSerializeToString(document));

            return xmlDoc.DocumentElement;
        }

        #endregion

        #endregion
    }
}
