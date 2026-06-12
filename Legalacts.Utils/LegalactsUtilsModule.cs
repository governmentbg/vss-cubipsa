using Legalacts.Utils.DocumentSerializer;
using Legalacts.Utils.XmlSchemaValidator;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Legalacts.Utils
{
    public class LegalactsUtilsModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDocumentSerializer>().To<DocumentSerializer.DocumentSerializer>().InRequestScope();
            Bind<IXmlSchemaValidator>().To<XmlSchemaValidator.XmlSchemaValidator>().InRequestScope();
        }
    }
}
