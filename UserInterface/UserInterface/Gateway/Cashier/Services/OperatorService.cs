using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UserInterface.MasterData;
using UserInterface.Helpers.Exceptions;
using System.Web.Http;
using System.Net.Http.Headers;

namespace UserInterface.Helpers.Services.MasterData
{
    public class OperatorService : IOperatorService
    {
        private const string BaseReadModelUrl = "http://localhost:8182/api";
        public readonly string BaseServiceUrl = "http://localhost:8183/api";

        private void ThrowIfServiceUnavailableError(HttpRequestException ex, string servicetype)
        {
            // "Unable to connect to the remote server"?
            if ((UInt32)ex.HResult == 0x80131500 && ex.InnerException != null && (UInt32)ex.InnerException.HResult == 0x80131509)
            {
                var response = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable) { Content = new StringContent(string.Format("MasterData {0} Service Unvailable", servicetype)), ReasonPhrase = "MasterData Service Unvailable" };
                throw new HttpResponseException(response);
            }
        }

        public async Task<IQueryable<OperatorDto>> GetOperatorListAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = new Uri(string.Format("{0}/operators", BaseReadModelUrl));
                    var response = await client.GetAsync(url);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException("There was an error retrieving the set of Operators.");
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var dtos = JsonConvert.DeserializeObject<IEnumerable<OperatorDto>>(result);
                    return dtos.AsQueryable();
                }
            }
            catch (HttpRequestException ex)
            {
                ThrowIfServiceUnavailableError(ex, "Read");
                throw;
            }
        }

        public async Task<OperatorDto> GetOperatorAsync(Guid id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = new Uri(string.Format("{0}/operators/{1}", BaseReadModelUrl, id));
                    var response = await client.GetAsync(url);
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new DtoNotFoundException(id, typeof(OperatorDto));
                    }

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException(string.Format("There was an error retriving the Operator with id {0}", id));
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var dto = JsonConvert.DeserializeObject<OperatorDto>(result);
                    return dto;
                }
            }
            catch (HttpRequestException ex)
            {
                ThrowIfServiceUnavailableError(ex, "Read");
                throw;
            }
        }

        public async Task<CreateOperatorResponse> CreateOperatorAsync(CreateOperatorCommand cmd)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = new Uri(string.Format("{0}/operators", BaseServiceUrl));
                    var response = await client.PostAsync<CreateOperatorCommand>(url, cmd, new JsonMediaTypeFormatter());
                    if (response.StatusCode != HttpStatusCode.Created)
                    {
                        throw new ApplicationException("There was an error creating a new Operator");
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var dto = JsonConvert.DeserializeObject<CreateOperatorResponse>(result);
                    return dto;
                }
            }
            catch (HttpRequestException ex)
            {
                ThrowIfServiceUnavailableError(ex, "Domain");
                throw;
            }
        }

        public async Task<AlterOperatorResponse> AlterOperatorAsync(Guid id, AlterOperatorCommand cmd)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = new Uri(string.Format("{0}/operators/{1}", BaseServiceUrl, id));
                    var response = await client.PutAsync<AlterOperatorCommand>(url, cmd, new JsonMediaTypeFormatter());
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException(string.Format("There was an error altering the Operator with id {0}", id));
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var dto = JsonConvert.DeserializeObject<AlterOperatorResponse>(result);
                    return dto;
                }
            }
            catch (HttpRequestException ex)
            {
                ThrowIfServiceUnavailableError(ex, "Domain");
                throw;
            }
        }

        public async Task<ArchiveOperatorResponse> ArchiveOperatorAsync(Guid id, ArchiveOperatorCommand cmd)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = new Uri(string.Format("{0}/operators/{1}/archive", BaseServiceUrl, id));
                    var response = await client.PutAsync<ArchiveOperatorCommand>(url, cmd, new JsonMediaTypeFormatter());
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException(string.Format("There was an error archiving the Operator with id {0}", id));
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var dto = JsonConvert.DeserializeObject<ArchiveOperatorResponse>(result);
                    return dto;
                }
            }
            catch (HttpRequestException ex)
            {
                ThrowIfServiceUnavailableError(ex, "Domain");
                throw;
            }
        }

        public async Task<DeleteOperatorResponse> DeleteOperatorAsync(Guid id, DeleteOperatorCommand cmd)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var url = new Uri(string.Format("{0}/operators/{1}", BaseServiceUrl, id));
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
                        throw new ApplicationException(string.Format("There was an error deleting Operator with id {0}", id));
                    }

                    var result = await response.Content.ReadAsStringAsync();
                    var dto = JsonConvert.DeserializeObject<DeleteOperatorResponse>(result);
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