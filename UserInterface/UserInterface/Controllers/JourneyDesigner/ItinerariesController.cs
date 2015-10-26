using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UserInterface.Helpers.Services;
using UserInterface.Commands;
using UserInterface.Models.Entities;
using System.Threading.Tasks;
using UserInterface.Gateway.JourneryDesigner.Commands.Itinerary;
using UserInterface.Helpers.ActionResults;
using UserInterface.Models.Commands;

namespace UserInterface.Controllers
{
    /// <summary>
    /// This controller acts as the API Gateway point for the Aggregate "Itinerary"
    /// </summary>
    public class ItinerariesController : ApiController
    {
        private readonly IItinerariesService itinerariesService;

        public ItinerariesController() : this(new ItinerariesService())
        {
        }

        public ItinerariesController(IItinerariesService itinerariesService)
        {
            this.itinerariesService = itinerariesService;
        }

        [HttpGet]
        [Route("api/itineraries", Name = "GetAllItineraries")]
        public async Task<IHttpActionResult> Get(int limit = 1000, int offset = 0, string seasonTerm = "")
        {
            var itineraries = (await itinerariesService.GetItinerariesAsync())
                .Where(i => !i.IsDeleted)
                .OrderByDescending(b => b.Id)
                .ToList();
            if (!string.IsNullOrWhiteSpace(seasonTerm))
            {
                // todo: dispatch this filter to GetProductListAsync()
                itineraries = itineraries.Where(x => x.SeasonId.ToString() == seasonTerm).ToList();
            }

            var totalCount = itineraries.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / limit);

            var results = itineraries
                         .Skip(offset)
                         .Take(limit)
                         .ToList();

            var response = new PagedOkResult<List<ItineraryDto>>(results, this)
            {
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return response;
        }

        [HttpGet]
        [Route("api/itineraries/{id:guid}", Name = "GetItineraryById")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var result = await itinerariesService.GetItineraryAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("api/itineraries", Name = "CreateItinerary")]
        public async Task<IHttpActionResult> CreateItinerary(CreateItineraryCommand cmd)
        {
            var result = await itinerariesService.CreateItineraryAsync(cmd);

            return CreatedAtRoute("GetItineraryById", new { id = result.Id }, result);
        }

        [HttpPut]
        [Route("api/itineraries/{id:guid}", Name = "EditItinerary")]
        public async Task<IHttpActionResult> EditItinerary(Guid id, ChangeItineraryCommand command)
        {
            var result = await itinerariesService.ChangeItineraryAsync(id, command);

            return Ok(result);
        }

        [HttpPut]
        [Route("api/itineraries/{id:guid}/delete", Name = "DeleteItinerary")]
        public async Task<IHttpActionResult> DeleteItinerary(Guid id, DeleteItineraryCommand command)
        {
            var result = await itinerariesService.DeleteItineraryAsync(id, command);

            return Ok(result);
        }

        [HttpPost]
        [Route("api/itineraries/{id:guid}/addday", Name = "AddDay")]
        public async Task<IHttpActionResult> AddDay(Guid id, AddDayToItineraryCommand cmd)
        {
            var result = await itinerariesService.AddDayToItineraryAsync(id, cmd);

            return Ok(result);
        }


        [HttpPut]
        [Route("api/itineraries/{id:guid}/reorderdays", Name = "ReorderDays")]
        public async Task<IHttpActionResult> ReorderDays(Guid id, ReorderDaysForItineraryCommand cmd)
        {
            var result = await itinerariesService.ReorderDaysForItineraryAsync(id, cmd);

            return Ok(result);
        }

        [HttpPut]
        [Route("api/itineraries/{id:guid}/removeday", Name = "RemoveDay")]
        public async Task<IHttpActionResult> RemoveDay(Guid id, RemoveDayFromItineraryCommand cmd)
        {
            var result = await itinerariesService.RemoveDayFromItineraryAsync(id, cmd);

            return Ok(result);
        }
    }
}
