using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Caching;
using System.Web.Mvc;

namespace Legalacts.Web.Captcha
{
    public static class HtmlExt
    {
        /// <summary>
        /// Generates the captcha image.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="height">The height.</param>
        /// <param name="width">The width.</param>
        /// <returns>
        /// Returns the <see cref="Uri"/> for the generated <see cref="CaptchaImage"/>.
        /// </returns>
        public static MvcHtmlString CaptchaImage(this HtmlHelper helper, int height, int width)
        {
            CaptchaImage image = new CaptchaImage
            {
                Height = height,
                Width = width,
            };

            HttpRuntime.Cache.Add(
                    image.UniqueId,
                    image,
                    null,
                    DateTime.Now.AddSeconds(Legalacts.Web.Captcha.CaptchaImage.CacheTimeOut),
                    Cache.NoSlidingExpiration,
                    CacheItemPriority.NotRemovable,
                    null);

            StringBuilder stringBuilder = new StringBuilder(256);
            stringBuilder.Append("<input type=\"hidden\" name=\"captcha-guid\" value=\"");
            stringBuilder.Append(image.UniqueId);
            stringBuilder.Append("\" />");
            stringBuilder.AppendLine();
            stringBuilder.Append("<img src=\"");
            stringBuilder.Append(helper.ViewContext.HttpContext.Request.ApplicationPath + "captcha.ashx?guid=" + image.UniqueId);
            stringBuilder.Append("\" alt=\"CAPTCHA\" width=\"");
            stringBuilder.Append(width);
            stringBuilder.Append("\" height=\"");
            stringBuilder.Append(height);
            stringBuilder.Append("\" style=\"vertical-align:top;\" />");

            return MvcHtmlString.Create(stringBuilder.ToString());
        }

    }
}
