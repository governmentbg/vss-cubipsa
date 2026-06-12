using Legalacts.Model.Repositories;
using Ninject;
using Legalacts.Model.Entities;
using System.Net.Http;
using System.Web.Http;
using Legalacts.Utils.DocumentSerializer;
using Legalacts.Utils.Managers.Pdf;
using System.Net;
using System.Net.Http.Headers;
using Legalacts.Utils.XmlSchemaValidator;
using System.Linq;
using System.Reflection;
using System.IO;

namespace Legalacts.ECLI.Converter.Controllers
{
    [RoutePrefix("api/v1/ECLI")]
    public class EcliController : ApiController
    {
        [Inject]
        public ILegalactsRepository _legalactsRepository { get; set; }

        [Inject]
        public IDocumentSerializer _documentSerializer { get; set; }

        [Inject]
        public IXmlSchemaValidator _xmlSchemaValidator { get; set; }

        public EcliController() { }

        [Route("convert"), HttpPost]
        public HttpResponseMessage Convert(Act act)
        {
            var document = ecli.Converter.CreateEcliDocument(act, Statics.LegalactsDomainName);

            var xml = _documentSerializer.XmlSerializeToString<ecli.document>(document);

            var path = Path.Combine(Directory.GetParent(Path.GetDirectoryName(
                        Assembly.GetExecutingAssembly().EscapedCodeBase.Substring(8))).FullName, "ecliSchemas");

            var errors = _xmlSchemaValidator.Validate(xml, path);

            if(errors.Count > 0)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent
                                (
                                    errors
                                        .Select(err => err.Item1)
                                        .Aggregate((err1, err2) => string.Format("{0}\n{1}", err1, err2))
                                ) 
                };
            }

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            result.Content = new StringContent(xml);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeTypeFileExtension.MIME_APPLICATION_XML);

            return result;
        }
    }
}
