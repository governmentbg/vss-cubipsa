using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Legalacts.Utils.DocumentSerializer
{
    /// <summary>
    /// Настройки на сериализатора на документи
    /// </summary>
    public class DocumentSerializerSettings
    {
        /// <summary>
        /// Тип на кодирането на стринговете
        /// </summary>
        public static Encoding DefaultEncoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }
}
