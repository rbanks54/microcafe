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
using UserInterface.Gateway.JourneryDesigner.Commands.Itinerary;
using UserInterface.Models;
using UserInterface.Models.Commands;

namespace UserInterface.Helpers.Services
{
    public class ItinerariesService : IItinerariesService
    {
        public readonly string BaseReadModelUrl = "http://localhost:8181/api";
        private const string BaseServiceUrl = "http://localhost:8180/api";


        public async Task<IEnumerable<ItineraryDto>> GetItinerariesAsync()
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/itineraries", BaseReadModelUrl));
                var response = await client.GetAsync(url);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException("There was an error retrieving the set of Master Itinerary.");
                }

                var result = await response.Content.ReadAsStringAsync();
                var dtos = JsonConvert.DeserializeObject<IEnumerable<ItineraryDto>>(result);
                return dtos.AsQueryable();
            }
        }

        public async Task<ItineraryDto> GetItineraryAsync(Guid id)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/itineraries/{1}", BaseReadModelUrl, id));
                var response = await client.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException(string.Format("Could not find master itinerary with id {0}", id));
                }

                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<ItineraryDto>(result);
                return dto;
            }
        }

        public async Task<CreateItineraryResponse> CreateItineraryAsync(CreateItineraryCommand cmd)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/itineraries", BaseServiceUrl));
                var response = await client.PostAsync<CreateItineraryCommand>(url, cmd, new JsonMediaTypeFormatter());
                if (response.StatusCode != HttpStatusCode.Created)
                {
                    throw new ApplicationException("Could not create master itinerary");
                }

                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<CreateItineraryResponse>(result);
                return dto;
            }
        }

        public async Task<ChangeItineraryResponse> ChangeItineraryAsync(Guid id, ChangeItineraryCommand command)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/itineraries/{1}", BaseServiceUrl, id));
                var response = await client.PutAsync<ChangeItineraryCommand>(url, command, new JsonMediaTypeFormatter());
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException(string.Format("There was an error changing the itinerary with id {0}", id));
                }

                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<ChangeItineraryResponse>(result);
                return dto;
            }
        }

        public async Task<DeleteItineraryResponse> DeleteItineraryAsync(Guid id, DeleteItineraryCommand command)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/itineraries/{1}/delete", BaseServiceUrl, id));
                var response = await client.PutAsync<DeleteItineraryCommand>(url, command, new JsonMediaTypeFormatter());
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException(string.Format("There was an error changing the itinerary with id {0}", id));
                }

                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<DeleteItineraryResponse>(result);
                return dto;
            }
        }

        public async Task<AddDayToItineraryResponse> AddDayToItineraryAsync(Guid id, AddDayToItineraryCommand command)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/itineraries/{1}/addday", BaseServiceUrl, id));
                var response = await client.PostAsync<AddDayToItineraryCommand>(url, command, new JsonMediaTypeFormatter());
                if (response.StatusCode != HttpStatusCode.Created)
                {
                    throw new ApplicationException(string.Format("There was an error changing the itinerary with id {0}", id));
                }

                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<AddDayToItineraryResponse>(result);
                return dto;
            }
        }


        public async Task<ReorderDaysForItineraryResponse> ReorderDaysForItineraryAsync(Guid id, ReorderDaysForItineraryCommand command)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/itineraries/{1}/reorderdays", BaseServiceUrl, id));
                var response = await client.PutAsync<ReorderDaysForItineraryCommand>(url, command, new JsonMediaTypeFormatter());
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException(string.Format("There was an error changing the itinerary with id {0}", id));
                }

                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<ReorderDaysForItineraryResponse>(result);
                return dto;
            }
        }


        public async Task<RemoveDayFromItineraryResponse> RemoveDayFromItineraryAsync(Guid id, RemoveDayFromItineraryCommand command)
        {
            using (var client = new HttpClient())
            {
                var url = new Uri(string.Format("{0}/itineraries/{1}/removeday", BaseServiceUrl, id));
                var response = await client.PutAsync<RemoveDayFromItineraryCommand>(url, command, new JsonMediaTypeFormatter());
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new ApplicationException(string.Format("There was an error changing the itinerary with id {0}", id));
                }

                var result = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<RemoveDayFromItineraryResponse>(result);
                return dto;
            }
        }
    }
}