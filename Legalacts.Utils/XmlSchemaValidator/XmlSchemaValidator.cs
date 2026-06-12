using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace Legalacts.Utils.XmlSchemaValidator
{
    /// <summary>
    /// Имплементация на компонента извършващ валидацията на XML според XML схемите на РИО
    /// </summary>
    public class XmlSchemaValidator : IXmlSchemaValidator
    {
        #region Public

        /// <summary>
        /// Конструктор на имплементацията
        /// </summary>
        public XmlSchemaValidator() { }

        /// <summary>
        /// Директория в която са записани схемите
        /// </summary>
        //public string ContentDirectory { get; set; }

        /// <summary>
        /// Валидира xml според схемите на РИО
        /// </summary>
        /// <param name="xml">съдържание на xml</param>
        /// <returns>true ако xml-а е валиден, иначе false</returns>
        public List<Tuple<string, string>> Validate(string xml, string schemasDirecotryPath)
        {
            //validating over the same XmlSchemaSet is NOT THREADSAFE
            lock (_syncRoot)
            {
                if (_xmlSchemaSet == null)
                {
                    _xmlSchemaSet = new XmlSchemaSet();
                    foreach (string schemaFile in Directory.GetFiles(schemasDirecotryPath, "*.xsd", SearchOption.AllDirectories))
                    {
                        using (TextReader schemaReader = new StreamReader(schemaFile))
                        {
                            _xmlSchemaSet.Add(null, XmlReader.Create(schemaReader));
                        }
                    }
                }

                XmlReader reader = null;
                List<Tuple<string, string>> errors = new List<Tuple<string, string>>();
#if DEBUG
                List<Tuple<string, string>> warnings = new List<Tuple<string, string>>();
#endif

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                settings.ValidationEventHandler +=
                    delegate(object sender, ValidationEventArgs args)
                    {
                        if (args.Severity == XmlSeverityType.Error)
                        {
                            errors.Add(Tuple.Create(args.Message, reader.Name));
                        }
#if DEBUG
                        else
                        {
                            warnings.Add(Tuple.Create(args.Message, reader.Name));
                        }
#endif
                    };
                settings.Schemas = _xmlSchemaSet;

                using(StringReader sr = new StringReader(xml))
                using (reader = XmlReader.Create(sr, settings))
                {
                    while (reader.Read())
                    {
                    }
                }

                int errorCount = errors.Count;
                if (errorCount > 0)
                {
                    string errorsText = 
                        errors
                        .Select(err => err.Item1)
                        .Aggregate((err1, err2) => string.Format("{0}\n{1}", err1, err2));

                    Debug.WriteLine($"Errors: {errorsText}");
                }
#if DEBUG
                if (warnings.Count > 0)
                {
                    string warningsText =
                        warnings
                        .Select(err => err.Item1)
                        .Aggregate((err1, err2) => string.Format("{0}\n{1}", err1, err2));

                    Debug.WriteLine($"Warnings: {warningsText}");
                }
#endif

                return errors;
            }
        }

        #endregion

        #region Private

        private static XmlSchemaSet _xmlSchemaSet = null;
        private object _syncRoot = new object();

        #endregion
    }
}
