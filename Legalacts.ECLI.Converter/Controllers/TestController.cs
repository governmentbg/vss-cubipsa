using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Legalacts.ECLI.Converter.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Index()
        {
            string result = "Service is running normally! Server time: " + DateTime.Now;

            var response = new HttpResponseMessage(HttpStatusCode.OK);

            response.Content = new StringContent(result, System.Text.Encoding.UTF8, "text/plain");

            return response;
        }
    }
}
