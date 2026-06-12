using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Legalacts.Utils
{
    public class Helper
    {
        public static List<int> GetIdListFromString(string idList)
        {
            List<int> ids = new List<int>();
            if (!string.IsNullOrEmpty(idList))
            {
                string[] values = idList.Split(',');

                foreach (string s in values)
                {
                    int i;
                    if (int.TryParse(s, out i))
                    {
                        ids.Add(i);
                    }
                }
            }

            return ids;
        }

        public static List<string> GetEmailListFromString(string input, char delimiter)
        {
            List<string> emails = new List<string>();

            if (!string.IsNullOrEmpty(input))
            {
                string[] values = input.Split(delimiter);

                foreach (var val in values)
                {
                    if (EmailCheck(val.Trim()))
                    {
                        emails.Add(val.Trim());
                    }
                }
            }

            return emails;
        }

        public static bool EmailCheck(string email)
        {
            string emailRegex = @"^[\w\-!#$%&'*+/=?^`{|}~.""]+@([\w]+[.-]?)+[\w]\.[\w]+$";

            return (!string.IsNullOrEmpty(email) && Regex.IsMatch(email, emailRegex));
        }

        public static string GetStringFromIdList(List<int> ids)
        {
            StringBuilder sb = new StringBuilder();
            if (ids.Any())
            {
                foreach (var id in ids)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(",");
                    }

                    sb.Append(id);
                }
            }

            return sb.ToString();
        }

        public static string ToNotNullString(string value)
        {
            return value != null ? value : String.Empty;
        }

        public static string FormatName(string firstName, string secondName, string lastName)
        {
            return String.Format("{0} {1} {2}", ToNotNullString(firstName), ToNotNullString(secondName), ToNotNullString(lastName)).Trim();
        }

        public static Tuple<string, string, string> SplitNames(string fullName)
        {
            string firstName = String.Empty;
            string secondName = String.Empty;
            string lastName = String.Empty;

            if (!String.IsNullOrWhiteSpace(fullName))
            {
                var splitName = fullName.Split(new Char[] { ' ' });

                firstName = splitName[0];
                if (splitName.Length > 1)
                {
                    lastName = splitName[splitName.Length - 1];

                    if (splitName.Length > 2)
                    {
                        for (int i = 1; i < splitName.Length - 1; i++)
                        {
                            secondName = secondName + splitName[i] + ' ';
                        }

                        secondName = secondName.Trim();
                    }
                }
            }

            return new Tuple<string, string, string>(firstName, secondName, lastName);
        }

        #region CastToSqlDbValue methods

        public static object CastToSqlDbValue(string value)
        {
            return value != null ? value : SqlString.Null;
        }

        public static object CastToSqlDbValue(int value)
        {
            return value;
        }

        public static object CastToSqlDbValue(int? value)
        {
            return value.HasValue ? value.Value : SqlInt32.Null;
        }

        public static object CastToSqlDbValue(bool value)
        {
            return value;
        }

        public static object CastToSqlDbValue(bool? value)
        {
            return value.HasValue ? value.Value : SqlBoolean.Null;
        }

        public static object CastToSqlDbValue(DateTime value)
        {
            return value;
        }

        public static object CastToSqlDbValue(DateTime? value)
        {
            return value.HasValue ? value.Value : SqlDateTime.Null;
        }

        public static object CastToSqlDbValue(Guid value)
        {
            return value;
        }

        public static object CastToSqlDbValue(Guid? value)
        {
            return value.HasValue ? value.Value : SqlGuid.Null;
        }

        public static object CastToSqlDbValue(byte[] value)
        {
            return value != null ? value : SqlBinary.Null;
        }

        #endregion

        public static byte[] StringToVersion(string version)
        {
            return Convert.FromBase64String(version);
        }

        public static string VersionToString(byte[] version)
        {
            return Convert.ToBase64String(version);
        }

        public static string CalculateSHA1(byte[] content)
        {
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            byte[] hashBytes = cryptoTransformSHA1.ComputeHash(content);
            return BitConverter.ToString(hashBytes).Replace("-", "");
        }

        public static string CreateExceptionString(Exception e)
        {
            StringBuilder sb = new StringBuilder();
            CreateExceptionString(sb, e, string.Empty);

            return sb.ToString();
        }

        private static void CreateExceptionString(StringBuilder sb, Exception e, string indent)
        {
            string _newLine = "\r\n";

            if (indent == null)
            {
                indent = string.Empty;
            }
            else if (indent.Length > 0)
            {
                sb.AppendFormat("{0}Inner ", indent);
            }

            sb.AppendFormat("Exception Found:{0}{1}Type: {2}", _newLine, indent, e.GetType().FullName);
            sb.AppendFormat("{0}{1}Message: {2}", _newLine, indent, e.Message);
            sb.AppendFormat("{0}{1}Source: {2}", _newLine, indent, e.Source);
            sb.AppendFormat("{0}{1}Stacktrace: {2}", _newLine, indent, e.StackTrace);

            if (e.InnerException != null)
            {
                sb.Append(_newLine);
                CreateExceptionString(sb, e.InnerException, indent + "  ");
            }
        }

        public static string RemoveXmlDeclarations(string xml)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            xmlDocument.ChildNodes.OfType<XmlNode>()
                        .Where(x => x.NodeType == XmlNodeType.XmlDeclaration)
                        .ToList()
                        .ForEach(x => xmlDocument.RemoveChild(x));

            return xmlDocument.OuterXml;
        }
    }
}
