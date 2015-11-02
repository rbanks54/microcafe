using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Cashier.Service.Controllers
{
    public static class ApiControllerHelper
    {
        /// <summary>
        /// Try Getting Commit Version from Command Request Header
        /// </summary>
        /// <param name="apiController"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static bool TryGetVersionFromHeader(this ApiController apiController, out int version)
        {
            if (apiController.Request.Headers.IfMatch == null || !apiController.Request.Headers.IfMatch.Any())
            {
                version = 0;
                return false;
            }

            EntityTagHeaderValue firstHeaderVal = apiController.Request.Headers.IfMatch.First();

            string value = firstHeaderVal.Tag;
            if (value.StartsWith("\""))
            {
                value = value.Substring(1);
            }
            if (value.EndsWith("\""))
            {
                value = value.Substring(0, value.Length - 1);
            }

            return int.TryParse(value, out version);
        }
    }
}
