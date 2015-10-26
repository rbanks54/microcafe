using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UserInterface.Models.Entities;

namespace UserInterface.Helpers.Services
{
    public class ReferenceDataService : IReferenceDataService
    {
        private const string BaseReadModelUrl = "http://localhost:8181/api";
        //private const string BaseServiceUrl = "http://localhost:8180/api";
        
        public async Task<IQueryable<BrandDto>> GetBrandListAsync()
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/brands", BaseReadModelUrl));
                var response = await client.GetAsync(url);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("There was an error retrieving the set of Brands");
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

        public async Task<IQueryable<OwnerDto>> GetOwnerListAsync()
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/owners", BaseReadModelUrl));
                var response = await client.GetAsync(url);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("There was an error retrieving the set of Owners");
                }

                var result = await response.Content.ReadAsStringAsync();
                var dtos = JsonConvert.DeserializeObject<IEnumerable<OwnerDto>>(result);
                return dtos.AsQueryable();
            }
        }

        public async Task<OwnerDto> GetOwnerAsync(Guid id)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/owners/{1}", BaseReadModelUrl, id));
                var response = await client.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ApplicationException(string.Format("Could not find Owner with id {0}", id));
                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException(string.Format("There was an error retriving the Owner with id {0}", id));
                }

                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<OwnerDto>(result);
                return dto;
            }
        }

        public async Task<IQueryable<OperatorDto>> GetOperatorListAsync()
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/operators", BaseReadModelUrl));
                var response = await client.GetAsync(url);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("There was an error retrieving the set of Operators");
                }

                var result = await response.Content.ReadAsStringAsync();
                var dtos = JsonConvert.DeserializeObject<IEnumerable<OperatorDto>>(result);
                return dtos.AsQueryable();
            }
        }

        public async Task<OperatorDto> GetOperatorAsync(Guid id)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/operators/{1}", BaseReadModelUrl, id));
                var response = await client.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ApplicationException(string.Format("Could not find Operator with id {0}", id));
                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException(string.Format("There was an error Operator the Product with id {0}", id));
                }

                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<OperatorDto>(result);
                return dto;
            }
        }

        public async Task<IQueryable<SeasonDto>> GetSeasonListAsync()
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/seasons", BaseReadModelUrl));
                var response = await client.GetAsync(url);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("There was an error retrieving the set of Seasons");
                }

                var result = await response.Content.ReadAsStringAsync();
                var dtos = JsonConvert.DeserializeObject<IEnumerable<SeasonDto>>(result);
                return dtos.AsQueryable();
            }
        }

        public async Task<SeasonDto> GetSeasonAsync(Guid id)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/seasons/{1}", BaseReadModelUrl, id));
                var response = await client.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ApplicationException(string.Format("Could not find Season with id {0}", id));
                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException(string.Format("There was an error retriving the Season with id {0}", id));
                }

                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<SeasonDto>(result);
                return dto;
            }
        }

        public async Task<IQueryable<ProductDto>> GetProductListAsync()
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/products", BaseReadModelUrl));
                var response = await client.GetAsync(url);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("There was an error retrieving the set of Products");
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
    }
}