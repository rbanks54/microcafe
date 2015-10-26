using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Barista.Common.Dto;
using MicroServices.Common.Exceptions;
using System.Threading;

namespace Barista.Common.Clients
{
    /// <summary>
    /// NOTES:
    /// 1. Should the loading be done async?
    /// </summary>
    public class BaristaClient : IBaristaClient
    {
        private const string masterDataServiceUrl = "http://localhost:8182/api/";
        private static ConcurrentDictionary<Guid, BrandDto> brands = new ConcurrentDictionary<Guid, BrandDto>();
        private static ConcurrentDictionary<Guid, OperatorDto> operators = new ConcurrentDictionary<Guid, OperatorDto>();

        public void Initialise()
        {
            InitialiseBrands();
            InitialiseOperators();
        }

        public void Reset()
        {
            ResetBrands();
            ResetOperators();
        }

        private static void ResetBrands()
        {
            brands = new ConcurrentDictionary<Guid, BrandDto>();
        }

        private static void ResetOperators()
        {
            operators = new ConcurrentDictionary<Guid, OperatorDto>();
        }

        private static void InitialiseOperators()
        {
            bool wasLoadingSuccessful = false;
            int maxNumberOfAttempts = 3;

            for (int attemptNumber = 0; attemptNumber < maxNumberOfAttempts; attemptNumber++)
            {
                try
                {
                    LoadOperators();

                    wasLoadingSuccessful = true;
                    break;
                }
                catch (WebException)
                {
                    // pause to allow the MasterData service to start up.
                    System.Threading.Thread.Sleep(1000);
                }

            }

            if (!wasLoadingSuccessful)
            {
                throw new ApplicationException("Failed to load the Operators from the Master Data service.");
            }
        }

        private static bool LoadOperators()
        {
            // 1. Make the http request to the other service
            string serviceUrl = masterDataServiceUrl + "operators";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "GET";

            // 2. Read the response
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var jsonResult = string.Empty;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                jsonResult = streamReader.ReadToEnd();
            }

            // 3. transform the json result. 
            // NOTE: it is possible for there to be a loss of data here.
            //       as we are only interest in our projection of the master data items here.
            var responseList = JsonConvert.DeserializeObject<List<OperatorDto>>(jsonResult);

            // 4. Load the data into the dictionary
            foreach (var operatorDto in responseList)
            {
                operators.TryAdd(operatorDto.Id, operatorDto);
            }

            // signal successful load
            return true;
        }

        public IEnumerable<OperatorDto> GetOperators()
        {
            return operators.Values.AsEnumerable();
        }

        public OperatorDto GetOperatorById(Guid operatorId)
        {
            OperatorDto result = null;
            if (operators.TryGetValue(operatorId, out result))
            {
                return result;
            }
            else
            {
                throw new ReadModelNotFoundException(operatorId, typeof(OperatorDto));
            }
        }

        public void InsertOrUpdateOperator(OperatorDto newValue)
        {
            OperatorDto retrievedValue;
            Guid searchKey = newValue.Id;

            if (operators.TryGetValue(searchKey, out retrievedValue))
            {
                // Replace the old value with the new value.
                if (!operators.TryUpdate(searchKey, newValue, retrievedValue))
                {
                    //The data was not updated. Log error, throw exception, etc.
                    throw new ApplicationException("Failed to update the operator");
                }
            }
            else
            {
                // Add the new key and value. 
                if (!operators.TryAdd(searchKey, newValue))
                {
                    throw new ApplicationException("Failed to add new operator");
                }
            }
        }

        public void DeleteOperator(Guid id)
        {
            OperatorDto deletedValue;

            if (!operators.TryRemove(id, out deletedValue))
            {
                throw new ApplicationException("Failed to delete operator");
            }
        }

        private static void InitialiseBrands()
        {
            bool wasLoadingSuccessful = false;
            int maxNumberOfAttempts = 3;

            for (int attemptNumber = 0; attemptNumber < maxNumberOfAttempts; attemptNumber++)
            {
                try
                {
                    LoadBrands();

                    wasLoadingSuccessful = true;
                    break;
                }
                catch (WebException)
                {
                    // pause to allow the MasterData service to start up.
                    System.Threading.Thread.Sleep(1000);
                }
                
            }

            if (!wasLoadingSuccessful)
            {
                throw new ApplicationException("Failed to load the Brands from the Master Data service.");
            }
        }

        private static bool LoadBrands()
        {
            // 1. Make the http request to the other service
            string serviceUrl = masterDataServiceUrl + "brands";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "GET";

            // 2. Read the response
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            var jsonResult = string.Empty;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                jsonResult = streamReader.ReadToEnd();
            }

            // 3. transform the json result. 
            // NOTE: it is possible for there to be a loss of data here.
            //       as we are only interest in our projection of the master data items here.
            var responseList = JsonConvert.DeserializeObject<List<BrandDto>>(jsonResult);

            // 4. Load the data into the dictionary
            foreach (var brandDto in responseList)
            {
                brands.TryAdd(brandDto.Id, brandDto);
            }

            // signal successful load
            return true;
        }
        
        public IEnumerable<BrandDto> GetBrands()
        {
            return brands.Values.AsEnumerable();
        }

        public BrandDto GetBrandById(Guid brandId)
        {
            BrandDto result = null;
            if (brands.TryGetValue(brandId, out result))
            {
                return result;
            }
            else
            {
                throw new ApplicationException("Cannot find the brand.");
            }
        }

        public void InsertOrUpdateBrand(BrandDto newValue)
        {
            BrandDto retrievedValue;
            Guid searchKey = newValue.Id;

            if (brands.TryGetValue(searchKey, out retrievedValue))
            {
                // Replace the old value with the new value.
                if (!brands.TryUpdate(searchKey, newValue, retrievedValue))
                {
                    //The data was not updated. Log error, throw exception, etc.
                    throw new ApplicationException("Failed to update the brand");
                }
            }
            else
            {
                // Add the new key and value. 
                if (!brands.TryAdd(searchKey, newValue))
                {
                    throw new ApplicationException("Failed to add new brand");
                }
            }
        }
    }
}
