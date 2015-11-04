using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MicroServices.Common.General.Util
{
    public class ApiClient
    {
        public T Load<T>(string url)
        {
            var webRequest = WebRequest.Create(url);
            webRequest.ContentType = "text/json";
            webRequest.Method = "GET";

            T result = default(T);

            var response = webRequest.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var jsonString = streamReader.ReadToEnd();
                result = JsonConvert.DeserializeObject<T>(jsonString);
            }

            return result;
        }

        public IList<T> LoadMany<T>(string url)
        {
            return Load<List<T>>(url);
        }

    }
}
