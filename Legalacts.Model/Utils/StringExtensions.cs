using System.Linq;

namespace Legalacts.Model.Utils
{
    public static class StringExtensions
    {
        public static char[] QuoteSymbols = new char[] { '"', '\'', '"', '´', 'ʹ', 'ʺ', 'ʻ', 'ʼ', 'ʽ', 'ʾ', 'ʿ', '˝', 'ˮ', '˵', '˶', '΄', '᾽', '῍', '᾿', '῎', '῝', '῞', '`', '´', '῾', '‘', '’', '‛', 
                                                        '“', '”', '„', '‟', '′', '″', '‴' };

        /// <summary>
        /// Огражда посочения string в кавички
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Quote(this string input)
        {
            return string.Format("\"{0}\"", input);
        }

        /// <summary>
        /// Проверява дали стринга започва и завършва с кавички
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsQuoted(this string input)
        {
            if (input.Length < 2)
                return false;

            string trimmed = input.Trim();

            char leading = trimmed[0];
            char last = trimmed[input.Length - 1];

            if (!QuoteSymbols.Contains(leading))
                return false;
            else
                return QuoteSymbols.Contains(last);
        }
    }
}
