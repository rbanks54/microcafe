using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using UserInterface.Helpers.ActionResults;
using UserInterface.Helpers.Services;
using UserInterface.Models.Entities;

namespace UserInterface.Controllers
{
    public class ReferenceDataController : ApiController
    {
        private IReferenceDataService referenceDataService;

        public ReferenceDataController() : this(new ReferenceDataService())
        {
        }

        public ReferenceDataController(IReferenceDataService referenceDataService)
        {
            this.referenceDataService = referenceDataService;
        }

        [HttpGet]
        [Route("api/referenceData/itineraryedit")]
        public async Task<IHttpActionResult> ItineraryEdit()
        {
            ItineraryEditRefData result = new ItineraryEditRefData();

            var brandResult = await referenceDataService.GetBrandListAsync();
            if (brandResult == null)
            {
                return new TimeOutResult("Brand serice is unavailable");
            }
            else
            {
                result.BrandDtos = brandResult.OrderBy(x => x.DisplayName);
            }

            var ownerResult = await referenceDataService.GetOwnerListAsync();
            if (ownerResult == null)
            {
                return new TimeOutResult("Owner serice is unavailable");
            }
            else
            {
                result.OwnerDtos = ownerResult.OrderBy(x => x.DisplayName);
            }

            var operatorResult = await referenceDataService.GetOperatorListAsync();
            if (operatorResult == null)
            {
                return new TimeOutResult("Operator serice is unavailable");
            }
            else
            {
                result.OperatorDtos = operatorResult.OrderBy(x => x.DisplayName);
            }

            var seasonResult = await referenceDataService.GetSeasonListAsync();
            if (seasonResult == null)
            {
                return new TimeOutResult("Season serice is unavailable");
            }
            else
            {
                result.SeasonDtos = seasonResult.OrderBy(x => x.DisplayName);
            }

            var productResult = await referenceDataService.GetProductListAsync();
            if (productResult == null)
            {
                return new TimeOutResult("Season serice is unavailable");
            }
            else
            {
                result.ProductDtos = productResult.OrderBy(x => x.DisplayName);
            }

            return Ok(result);
        }


        [HttpGet]
        [Route("api/referenceData/itineraries")]
        public async Task<IHttpActionResult> Itineraries()
        {
            ItinerariesRefData result = new ItinerariesRefData();

            var seasonResult = await referenceDataService.GetSeasonListAsync();
            if (seasonResult == null)
            {
                return new TimeOutResult("Season serice is unavailable");
            }
            else
            {
                result.SeasonDtos = seasonResult;
            }


            return Ok(result);
        }
    }
}