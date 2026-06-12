using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Legalacts.ECLI.Integrator
{
    public class Statics
    {
        #region Public

        public readonly static string LAST_INDEXED_KEY = "LAST_INDEXED_KEY";
        public readonly static string ZIP_KEY = "ZIP_KEY";
        public readonly static string SITEMAP_KEYWORD = "sitemap";
        public readonly static string INDEX_KEYWORD = "index";
        public readonly static List<int> ALLOWED_ACT_KINDS = new List<int>() { 5001, 5002, 5003 };
        public readonly static DateTime ROUTE_INDEX_DATE = new DateTime(2007, 1, 1);

        public static int IndexingJobIntervalInSeconds
        {
            get
            {
                return GetAppConfigValue<int>("IndexingJobIntervalInSeconds");
            }
        }

        public static int IndexingJobBatchCount
        {
            get
            {
                return GetAppConfigValue<int>("IndexingJobBatchCount");
            }
        }

        public static string DomainName
        {
            get
            {
                var domain = GetAppConfigValue<string>("DomainName");

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
