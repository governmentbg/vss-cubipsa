using ecli;
using ecli_types;
using Legalacts.Model.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ecli
{
    public class Converter
    {
        public static document CreateEcliDocument(Act act, string domain, statusType statusType = statusType.active)
        {
            var doc = new document()
            {
                status = statusType,
                metadata = new metadataType()
                {
                    identifierCollection = new identifierWithGroupTypeCollection(),
                    isVersionOf = new ecliType()
                    {
                        country = countryType.BG,
                        court = act.Court.EcliCode,
                        value = act.PreviousEcliCode ?? act.EcliCode
                    },
                    creatorCollection = new notEmptyTypeWithLangAndGroupCollection(),
                    coverageCollection = new notEmptyTypeWithLangAndGroupCollection(),
                    date = new Iso8601SerializableDateTimeOffset(new DateTimeOffset(act.StartDate.Value)), //Датата следва да е записана съгласно изискванията на ISO 8601 пример: (2017-02-15)
                    languageCollection = new languageWithGroupTypeCollection(),
                    publisherCollection = new notEmptyTypeWithLangAndGroupCollection(),
                    accessRights = "public",
                    type = new typeType()
                    {
                        MixedValue = ConvertActKindName(act.ActKind.Name),
                        lang = ecli_types.languageType.bg
                    },

                    titleCollection = new notEmptyTypeWithLangAndGroupCollection(),
                    issued = new Iso8601SerializableDateTimeOffset(new DateTimeOffset(act.CreateDate.Value)) //Датата следва да е записана съгласно изискванията на ISO 8601 пример: (2017-02-15)
                }
            };

            if (!string.IsNullOrWhiteSpace(act.PreviousEcliCode))
            {
                doc.metadata.isReplacedBy = new ecliType()
                {
                    country = countryType.BG,
                    court = act.Court.EcliCode,
                    value = act.EcliCode,
                };
            }

            if (act.ActDocumentId != null)
            {
                var actIdentifier = new identifierWithGroupType()
                {
                    MixedValue = $"{domain}/GetActContent/" + act.EcliCode,
                    lang = ecli_types.languageType.bg,
                    format = GetActDocumentMimeType(act.ActDocument.MimeType)
                };

                doc.metadata.identifierCollection.Add(actIdentifier);
            }

            if (act.MotiveDocumentId != null)
            {
                var motiveIdentifier = new identifierWithGroupType()
                {
                    MixedValue = $"{domain}/GetMotiveContent/" + act.EcliCode,
                    lang = ecli_types.languageType.bg,
                    format = GetActDocumentMimeType(act.MotiveDocument.MimeType)
                };

                doc.metadata.identifierCollection.Add(motiveIdentifier);
            }

            doc.metadata.identifierCollection.Add(new identifierWithGroupType()
            {
                MixedValue = $"{domain}/" + act.EcliCode,
                lang = ecli_types.languageType.bg,
                format = ecli_types.formatType.text_html
            });

            var creator = new notEmptyTypeWithLangAndGroup()
            {
                MixedValue = act.Court.Name,
                lang = ecli_types.languageType.bg
            };

            doc.metadata.creatorCollection.Add(creator);

            var coverage = new notEmptyTypeWithLangAndGroup()
            {
                MixedValue = "България",
                lang = ecli_types.languageType.bg
            };

            doc.metadata.coverageCollection.Add(coverage);

            var language = new languageWithGroupType()
            {
                MixedValue = "bg",
                languageTypeType = "authoritative"
            };

            doc.metadata.languageCollection.Add(language);

            var publisher = new notEmptyTypeWithLangAndGroup()
            {
                MixedValue = domain,
                lang = ecli_types.languageType.bg
            };

            doc.metadata.publisherCollection.Add(publisher);

            var title = new notEmptyTypeWithLangAndGroup()
            {
                MixedValue = GenerateActTitile(act),
                lang = ecli_types.languageType.bg
            };

            doc.metadata.titleCollection.Add(title);

            if (!string.IsNullOrWhiteSpace(act.Judge))
            {
                doc.metadata.contributorCollection = new notEmptyTypeWithLangAndGroupCollection()
                {
                    new notEmptyTypeWithLangAndGroup()
                    {
                        MixedValue = ProcessContributorsData(act.Judge),
                        lang = ecli_types.languageType.bg
                    }
                };
            }

            if (act.ConnectedActs.Any())
            {
                doc.metadata.referenceCollection = new referenceWithGroupTypeCollection();
                foreach (var connectedAct in act.ConnectedActs)
                {
                    var reference = new ReferenceWithGroupType()
                    {
                        MixedValue = act.EcliCode,
                        type = ecli_types.referenceType.ECLI,
                        relation = act.StartDate > connectedAct.StartDate ? ecli_types.relationType.followedBy : ecli_types.relationType.precededBy,
                        lang = ecli_types.languageType.bg,
                    };

                    doc.metadata.referenceCollection.Add(reference);
                }
            }

            return doc;
        }

        private static ecli_types.formatType GetActDocumentMimeType(string mimeType)
        {
            var formatType = new ecli_types.formatType();
            switch (mimeType)
            {
                case "text/plain":
                    formatType = ecli_types.formatType.text_plain;
                    break;
                case "text/html":
                    formatType = ecli_types.formatType.text_html;
                    break;
                case "application/msword":
                    formatType = ecli_types.formatType.application_msword;
                    break;
                case "application/pdf":
                    formatType = ecli_types.formatType.application_pdf;
                    break;
                default:
                    break;
            };

            return formatType;
        }

        private static string ConvertActKindName(string name)
        {
            var type = string.Empty;
            switch (name)
            {
                case "Решение":
                case "Присъда":
                    type = "Съдебно решение";
                    break;
                case "Определение":
                    type = "Съдебно определение";
                    break;
                case "Становище":
                    type = "Становище";
                    break;
                default:
                    type = "Съдебен акт";
                    break;
            }

            return type;
        }

        private static string GenerateActTitile(Act act)
        {
            var date = act.StartDate.HasValue ? act.StartDate.Value.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) : "";
            var actNumber = act.ActNumber.HasValue ? "№" + act.ActNumber + " " : string.Empty;
            var title = $"{act.ActKind.Name} {actNumber}от {date} г. на {act.Court.Name} по {act.CaseKind.Abbreviation} № {act.CaseNumber}/{act.CaseYear} г.";

            return title;
        }

        private static string ProcessContributorsData(string contributors)
        {
            if (string.IsNullOrWhiteSpace(contributors))
                return string.Empty;

            var serviceSymbols = new Dictionary<string, string>()
            {
                { "<br>", string.Empty},
                { "</br>", string.Empty},
                { "</ br>", string.Empty},
                { "&", "&amp;"},
                { "'", "&apos;"},
                { ">", "&gt;"},
                { "<", "&lt;"}
            };

            var processedContributors = contributors;

            foreach (var symbol in serviceSymbols)
            {
                processedContributors = processedContributors.Replace(symbol.Key, symbol.Value);
            }

            return processedContributors;
        }
    }
}