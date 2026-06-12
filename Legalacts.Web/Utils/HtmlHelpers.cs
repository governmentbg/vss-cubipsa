using System.Security.Policy;
using Legalacts.Utils.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Dynamic;

namespace Legalacts.Web.Utils
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString CustomValidationSummary(this HtmlHelper helper, bool hasBorder = false)
        {
            string retVal = "";
            if (helper.ViewData.ModelState.IsValid)
                return MvcHtmlString.Create(retVal);
            retVal += @"<div class='rw'>
                            <div class='lbl'></div>
                            <div class='vl'>";
            if (hasBorder)
            {
                retVal += "<ul class='validation-errors border'>";
            }
            else
            {
                retVal += "<ul class='validation-errors'>";
            }

            foreach (var key in helper.ViewData.ModelState.Keys)
            {
                foreach (var err in helper.ViewData.ModelState[key].Errors)
                    retVal += "<li>" + helper.Encode(err.ErrorMessage) + "</li>";
            }
            retVal += "</div></div>";
            return MvcHtmlString.Create(retVal);
        }

        public static MvcHtmlString EncryptActionLink(this HtmlHelper helper, 
            string linkText, 
            string actionName, 
            string controllerName, 
            object routeValues, 
            object htmlAttributes)
        {
            RouteValueDictionary rvd = new RouteValueDictionary(routeValues);
            RouteValueDictionary retRvd = new RouteValueDictionary(routeValues);
            
            foreach (var rv in rvd)
            {
                retRvd[rv.Key] =  (object)ConfigurationBasedStringEncrypter.Encrypt(rv.Value.ToString());
            }

            return LinkExtensions.ActionLink(helper, linkText, actionName, controllerName, 
                retRvd, (new RouteValueDictionary(htmlAttributes)));
        }

        public static string EncryptAction(this UrlHelper helper,
            string actionName,
            string controllerName,
            object routeValues)
        {
            var rvd = new RouteValueDictionary(routeValues);
            RouteValueDictionary retRvd = new RouteValueDictionary(routeValues);

            foreach (var rv in rvd)
            {
                retRvd[rv.Key] = (object)ConfigurationBasedStringEncrypter.Encrypt(rv.Value.ToString());
            }

            return helper.Action(actionName, controllerName, retRvd);
        }
    }
}