using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Legalacts.Web.Utils
{
    public class Statics
    {
        #region Public

        public static int MaxActItemsPerPage
        {
            get
            {
                return GetAppConfigValue<int>("MaxActItemsPerPage");
            }
        }

        public static int MaxActItems
        {
            get
            {
                return GetAppConfigValue<int>("MaxActItems");
            }
        }

        public static int MaxLogItemsPerPage
        {
            get
            {
                return GetAppConfigValue<int>("MaxLogItemsPerPage");
            }
        }

        public static int MaxLogItems
        {
            get
            {
                return GetAppConfigValue<int>("MaxLogItems");
            }
        }

        public static int ConnectedActsJobIntervalInHours
        {
            get
            {
                return GetAppConfigValue<int>("ConnectedActsJobIntervalInHours");
            }
        }

        public static int MailSenderJobIntervalInMinutes
        {
            get
            {
                return GetAppConfigValue<int>("MailSenderJobIntervalInMinutes");
            }
        }

        public static string FeedbackEmails
        {
            get
            {
                return GetAppConfigValue<string>("FeedbackEmails");
            }
        }

        public static bool EnablePdfConverting
        {
            get
            {
                return GetAppConfigValue<bool>("EnablePdfConverting");
            }
        }

        public static int AccessDenyLimit
        {
            get
            {
                return GetAppConfigValue<int>("AccessDenyLimit");
            }
        }

        public static int AccessDenyLimitDurationInDays
        {
            get
            {
                return GetAppConfigValue<int>("AccessDenyLimitDurationInDays");
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
