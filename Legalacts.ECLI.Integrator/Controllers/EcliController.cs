using Legalacts.Model.Repositories;
using Ninject;
using Legalacts.Model.Entities;
using System;
using System.Net.Http;
using System.Web.Http;
using Legalacts.Utils.DocumentSerializer;
using Legalacts.Utils.Managers.Pdf;
using System.Net;
using System.Net.Http.Headers;
using Legalacts.Utils.XmlSchemaValidator;
using System.Linq;
using System.Data.Entity;
using System.Reflection;
using System.IO;
using Legalacts.Model.Repositories.SitemapRepository;
using System.Text;
using Legalacts.Utils.Managers;
using System.Collections.Generic;

namespace Legalacts.ECLI.Integrator.Controllers
{
    public class EcliController : ApiController
    {
        [Inject]
        public ILegalactsRepository _legalactsRepository { get; set; }

        [Inject]
        public ISitemapRepository _sitemapRepository { get; set; }

        [Inject]
        public IDocumentSerializer _documentSerializer { get; set; }

        [Inject]
        public IXmlSchemaValidator _xmlSchemaValidator { get; set; }

        public EcliController() { }

        [HttpGet]
        [Route("robots.txt")]
        public HttpResponseMessage RobotsText()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("User-agent: *");
            stringBuilder.AppendLine("Disallow: /");
            stringBuilder.AppendLine("User-agent: DG_JUSTICE_CRAWLER");
            stringBuilder.AppendLine("Allow: /");

            DateTime firstDate = DateTime.Now.AddYears(-1);

            var indexes = _sitemapRepository
                                .GetAllIndexes()
                                .Include(e => e.Route)
                                .Where(e => e.Route.Date >= firstDate)
                                .OrderBy(e => e.Route.Date)
                                .ToList();

            foreach (var index in indexes)
            {
                var date = index.Route.Date;

                var excludeDates = new List<DateTime>() { new DateTime(2023, 12, 15) };

                if (!excludeDates.Contains(date))
                {
                    stringBuilder.AppendLine($"Sitemap: {Statics.DomainName}/{date.Year}/{date.Month.ToString("00")}/{date.Day.ToString("00")}/{index.Name}.xml");
                }
            }

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            result.Content = new StringContent(stringBuilder.ToString());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypeFileExtension.MIME_TEXT_PLAIN);

            return result;

        }

        [Route("{year}/{month}/{day}/{name}.xml"), HttpGet]
        public HttpResponseMessage Sitemap(int year, int month, int day, string name)
        {
            var date = new DateTime(year, month, day);

            var route = _sitemapRepository.GetAllRoutes()
                            .Include(e => e.Indexes)
                            .Include(e => e.Indexes.Select(i => i.Sitemaps))
                            .FirstOrDefault(e => e.Date == date);

            if (route == null)
            {
                ThrowHttpException(HttpStatusCode.NotFound, "No such route!");
            }

            var index = route.Indexes.FirstOrDefault();

            if (index == null)
            {
                ThrowHttpException(HttpStatusCode.NotFound, "Route has no index!");
            }

            var xml = string.Empty;

            if (name.Contains(Statics.INDEX_KEYWORD))
            {
                var bytes = ZipManager.Decompress(index.XML);
                xml = Encoding.UTF8.GetString(bytes);
            }
            else
            {
                var sitemap = index.Sitemaps.FirstOrDefault();

                if (sitemap == null)
                {
                    ThrowHttpException(HttpStatusCode.NotFound, "Route index has no sitemap!");
                }

                var bytes = ZipManager.Decompress(sitemap.XML);
                xml = Encoding.UTF8.GetString(bytes);
            }

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            result.Content = new StringContent(xml);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypeFileExtension.MIME_APPLICATION_XML);

            return result;
        }

        private void ThrowHttpException(HttpStatusCode status, string message)
        {
            throw new HttpResponseException
                    (new HttpResponseMessage(status)
                    { Content = new StringContent(message, Encoding.UTF8) });
        }
    }
}
