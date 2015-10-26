using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using UserInterface.Commands;
using UserInterface.CommandResponses;
using UserInterface.Models.Entities;
using System.Threading.Tasks;
using System.Net.Http.Formatting;
using UserInterface.Models.CommandResponses;
using UserInterface.Models.JourneryDesigner.Commands;

namespace UserInterface.Helpers.Services
{
    public class ProductsService : IProductsService
    {
        private const string BaseReadModelUrl = "http://localhost:8181/api";
        public readonly string BaseServiceUrl = "http://localhost:8180/api";

        public async Task<IQueryable<ProductDto>> GetProductListAsync()
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/products", BaseReadModelUrl));
                var response = await client.GetAsync(url);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("There was an error retrieving the set of Products.");
                }

                var result = await response.Content.ReadAsStringAsync();
                var dtos = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(result);
                return dtos.AsQueryable();
            }
        }

        public async Task<ProductDto> GetProductAsync(Guid id)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/products/{1}", BaseReadModelUrl, id));
                var response = await client.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ApplicationException(string.Format("Could not find Product with id {0}", id));
                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException(string.Format("There was an error retriving the Product with id {0}", id));
                }

                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<ProductDto>(result);
                return dto;
            }
        }

        public async Task<bool> ProductExists(string code)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/products/exists/{1}", BaseReadModelUrl, code));
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsAsync<bool>();
                return result;
            }
        }

        public async Task<CreateProductResponse> CreateProductAsync(CreateProductCommand cmd)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/products", BaseServiceUrl));
                var response = await client.PostAsync<CreateProductCommand>(url, cmd, new JsonMediaTypeFormatter());
                if (response.StatusCode != HttpStatusCode.Created)
                {
                    throw new ApplicationException("There was an error creating a new Product");
                }

                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<CreateProductResponse>(result);
                return dto;
            }
        }

        public async Task<AlterProductResponse> AlterProductAsync(Guid id, AlterProductCommand cmd)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/products/{1}", BaseServiceUrl, id));
                var response = await client.PutAsync<AlterProductCommand>(url, cmd, new JsonMediaTypeFormatter());
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException(string.Format("There was an error altering the Product with id {0}", id));
                }

                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<AlterProductResponse>(result);
                return dto;
            }
        }
    }
}