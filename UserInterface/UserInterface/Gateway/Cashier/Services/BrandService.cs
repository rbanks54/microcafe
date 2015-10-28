using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UserInterface.MasterData;

namespace UserInterface.Helpers.Services.MasterData
{
    public class BrandService : IBrandService
    {
        private const string BaseReadModelUrl = "http://localhost:8182/api";
        public readonly string BaseServiceUrl = "http://localhost:8183/api";

        public async Task<IQueryable<BrandDto>> GetBrandListAsync()
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/brands", BaseReadModelUrl));
                var response = await client.GetAsync(url);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("There was an error retrieving the set of Brands.");
                }

                var result = await response.Content.ReadAsStringAsync();
                var dtos = JsonConvert.DeserializeObject<IEnumerable<BrandDto>>(result);
                return dtos.AsQueryable();
            }
        }

        public async Task<BrandDto> GetBrandAsync(Guid id)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/brands/{1}", BaseReadModelUrl, id));
                var response = await client.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ApplicationException(string.Format("Could not find Brand with id {0}", id));
                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException(string.Format("There was an error retriving the Brand with id {0}", id));
                }

                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<BrandDto>(result);
                return dto;
            }
        }

        public async Task<CreateBrandResponse> CreateBrandAsync(CreateBrandCommand cmd)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/brands", BaseServiceUrl));
                var response = await client.PostAsync<CreateBrandCommand>(url, cmd, new JsonMediaTypeFormatter());
                if (response.StatusCode != HttpStatusCode.Created)
                {
                    var responseMessage = response.Content.ReadAsStringAsync();

                    var cleanResponse = responseMessage.Result.Replace("\\r\\n", "<br/>");

                    string exceptionJson = "{ \"FriendlyMessage\": \"There was an error creating a new Brand.\", \"Data\": " + cleanResponse + "}";
                    
                    throw new ApplicationException(exceptionJson);
                }

                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<CreateBrandResponse>(result);
                return dto;
            }
        }

        public async Task<AlterBrandResponse> AlterBrandAsync(Guid id, AlterBrandCommand cmd)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/brands/{1}", BaseServiceUrl, id));
                var response = await client.PutAsync<AlterBrandCommand>(url, cmd, new JsonMediaTypeFormatter());
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException(string.Format("There was an error renaming the Brand with id {0}", id));
                }

                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<AlterBrandResponse>(result);
                return dto;
            }
        }
    }
}