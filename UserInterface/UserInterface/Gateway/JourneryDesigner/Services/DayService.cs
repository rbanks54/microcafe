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
using UserInterface.Models;
using UserInterface.Models.Commands;
using System.Web.Http;
using UserInterface.Data;
using UserInterface.Helpers.Exceptions;
using System.Net.Http.Headers;

namespace UserInterface.Helpers.Services
{
    public class DayService : IDayService
    {
        private const string BaseReadModelUrl = "http://localhost:8181/api";
        public readonly string BaseServiceUrl = "http://localhost:8180/api";

        private void ThrowIfServiceUnavailableError(HttpRequestException ex, string servicetype)
        {
            // "Unable to connect to the remote server"?
            if ((UInt32)ex.HResult == 0x80131500 && ex.InnerException != null && (UInt32)ex.InnerException.HResult == 0x80131509)
            {
                var response = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable) { Content = new StringContent(string.Format("JourneyDesigner {0} Service Unvailable", servicetype)), ReasonPhrase = "JourneyDesigner Service Unvailable" };
                throw new HttpResponseException(response);
            }
        }

        public async Task<IQueryable<DayDto>> GetDayListAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = new Uri(string.Format("{0}/days", BaseReadModelUrl));
                    var response = await client.GetAsync(url);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException("There was an error retrieving the set of Days.");
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var dtos = JsonConvert.DeserializeObject<IEnumerable<DayDto>>(result);
                    return dtos.AsQueryable();
                }
            }
            catch (HttpRequestException ex)
            {
                ThrowIfServiceUnavailableError(ex, "Read");
                throw;
            }
        }

        public async Task<DayDto> GetDayAsync(Guid id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = new Uri(string.Format("{0}/days/{1}", BaseReadModelUrl, id));
                    var response = await client.GetAsync(url);
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new DtoNotFoundException(id, typeof(DayDto));
                    }

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException(string.Format("There was an error retriving the Day with id {0}", id));
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var dto = JsonConvert.DeserializeObject<DayDto>(result);
                    return dto;
                }
            }
            catch (HttpRequestException ex)
            {
                ThrowIfServiceUnavailableError(ex, "Read");
                throw;
            }
        }

        public async Task<CreateDayResponse> CreateDayAsync(CreateDayCommand cmd)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = new Uri(string.Format("{0}/days", BaseServiceUrl));
                    var response = await client.PostAsync<CreateDayCommand>(url, cmd, new JsonMediaTypeFormatter());
                    if (response.StatusCode != HttpStatusCode.Created)
                    {
                        throw new ApplicationException("There was an error creating a new Day");
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var dto = JsonConvert.DeserializeObject<CreateDayResponse>(result);
                    return dto;
                }
            }
            catch (HttpRequestException ex)
            {
                ThrowIfServiceUnavailableError(ex, "Domain");
                throw;
            }
        }

        public async Task<ChangeDayResponse> ChangeDayAsync(Guid id, ChangeDayCommand cmd)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = new Uri(string.Format("{0}/days/{1}", BaseServiceUrl, id));
                    var response = await client.PutAsync<ChangeDayCommand>(url, cmd, new JsonMediaTypeFormatter());
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException(string.Format("There was an error altering the Day with id {0}", id));
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var dto = JsonConvert.DeserializeObject<ChangeDayResponse>(result);
                    return dto;
                }
            }
            catch (HttpRequestException ex)
            {
                ThrowIfServiceUnavailableError(ex, "Domain");
                throw;
            }
        }

        public async Task<DeleteDayResponse> DeleteDayAsync(Guid id, DeleteDayCommand cmd)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = new Uri(string.Format("{0}/days/{1}", BaseServiceUrl, id));
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = url,
                        Method = HttpMethod.Delete,
                    };
                    request.Headers.IfMatch.Add(new EntityTagHeaderValue("\"" + cmd.Version.ToString() + "\""));

                    var response = await client.SendAsync(request);

                    // If-Match Header Precondition Failed
                    if (response.StatusCode == HttpStatusCode.PreconditionFailed)
                    {
                        throw new CommandVersionException(id, cmd.GetType(), cmd.Version);
                    }

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException(string.Format("There was an error deleting Day with id {0}", id));
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var dto = JsonConvert.DeserializeObject<DeleteDayResponse>(result);
                    return dto;
                }
            }
            catch (HttpRequestException ex)
            {
                ThrowIfServiceUnavailableError(ex, "Domain");
                throw;
            }
        }

        public async Task<AddCostItemToDayResponse> AddCostItemToDayAsync(Guid id, AddCostItemToDayCommand cmd)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = new Uri(string.Format("{0}/days/{1}/costitems", BaseServiceUrl, id));

                    var response = await client.PostAsync<AddCostItemToDayCommand>(url, cmd, new JsonMediaTypeFormatter());
                    if (response.StatusCode != HttpStatusCode.Created)
                    {
                        throw new ApplicationException(string.Format("There was an error adding CostItem to Day {1}", id));
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var dto = JsonConvert.DeserializeObject<AddCostItemToDayResponse>(result);
                    return dto;
                }
            }
            catch (HttpRequestException ex)
            {
                ThrowIfServiceUnavailableError(ex, "Domain");
                throw;
            }
        }

        public async Task<RemoveCostItemFromDayResponse> RemoveCostItemFromDayAsync(Guid id, RemoveCostItemFromDayCommand cmd)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = new Uri(string.Format("{0}/days/{1}/costitems/{2}", BaseServiceUrl, id, cmd.CostItemId));
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = url,
                        Method = HttpMethod.Delete,
                    };
                    request.Headers.IfMatch.Add(new EntityTagHeaderValue("\"" + cmd.Version.ToString() + "\""));

                    var response = await client.SendAsync(request);

                    // If-Match Header Precondition Failed
                    if (response.StatusCode == HttpStatusCode.PreconditionFailed)
                    {
                        throw new CommandVersionException(id, cmd.GetType(), cmd.Version);
                    }

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException(string.Format("There was an error removing CostItem {0} from Day {1}", cmd.CostItemId, id));
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var dto = JsonConvert.DeserializeObject<RemoveCostItemFromDayResponse>(result);
                    return dto;
                }
            }
            catch (HttpRequestException ex)
            {
                ThrowIfServiceUnavailableError(ex, "Domain");
                throw;
            }
        }

        public async Task<ReorderCostItemsForDayResponse> ReorderCostItemsForDayAsync(Guid id, ReorderCostItemsForDayCommand cmd)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = new Uri(string.Format("{0}/days/{1}/reordercostitems", BaseServiceUrl, id));

                    var response = await client.PutAsync<ReorderCostItemsForDayCommand>(url, cmd, new JsonMediaTypeFormatter());
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException(string.Format("There was an error reordering CostItems for Day {1}", id));
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var dto = JsonConvert.DeserializeObject<ReorderCostItemsForDayResponse>(result);
                    return dto;
                }
            }
            catch (HttpRequestException ex)
            {
                ThrowIfServiceUnavailableError(ex, "Domain");
                throw;
            }
        }

    }
}