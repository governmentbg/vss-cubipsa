using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Legalacts.ECLI.Converter
{
    public class Statics
    {
        #region Public

        public static string LegalactsDomainName
        {
            get
            {
                var domain = GetAppConfigValue<string>("LegalactsDomainName");

                if (!string.IsNullOrWhiteSpace(domain))
                {
                    return domain.Last() == '/' ? domain.Substring(0, domain.Length - 1) : domain;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        #endregion

        #region Private

        private static ConcurrentDictionary<string, object> _valueCache = new ConcurrentDictionary<string, object>();
        private static object _syncRoot = new object();

        private static T GetAppConfigValue<T>(string appConfigKey)
        {
            if (!_valueCache.ContainsKey(appConfigKey))
            {
                lock (_syncRoot)
                {
                    if (!_valueCache.ContainsKey(appConfigKey))
                    {
                        string appConfigValue = System.Configuration.ConfigurationManager.AppSettings[appConfigKey];

                        T configValue = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(appConfigValue);

                        _valueCache.TryAdd(appConfigKey, configValue);
                    }
                }
            }

            return (T)_valueCache[appConfigKey];
        }

        #endregion
    }
}
