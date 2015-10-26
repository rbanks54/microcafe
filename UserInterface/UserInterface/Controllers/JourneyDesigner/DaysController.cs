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
using UserInterface.Helpers.Exceptions;
using UserInterface.Data;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;

namespace UserInterface.Controllers
{
    /// <summary>
    /// This controller acts as the API Gateway point for the Aggregate "Day"
    /// </summary>
    public class DaysController : ApiController
    {
        private readonly IDayService dayService;

        public DaysController() : this(new DayService())
        {
        }

        public DaysController(IDayService dayService)
        {
            this.dayService = dayService;
        }

        private bool TryGetVersionFromHeader(out int version)
        {
            if (Request.Headers.IfMatch == null || !Request.Headers.IfMatch.Any())
            {
                version = 0;
                return false;
            }

            EntityTagHeaderValue firstHeaderVal = Request.Headers.IfMatch.First();

            string value = firstHeaderVal.Tag;
            if (value.StartsWith("\""))
            {
                value = value.Substring(1);
            }
            if (value.EndsWith("\""))
            {
                value = value.Substring(0, value.Length - 1);
            }

            return Int32.TryParse(value, out version);
        }

        [HttpGet]
        [Route("api/days/{id:guid}", Name = "GetDayById")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            DayDto result = null;

            try
            {
                result = await dayService.GetDayAsync(id);
            }
            catch (AggregateException ex)
            {
                if (ex.InnerExceptions.Any(e => e is DtoNotFoundException))
                    return NotFound();

                throw;
            }

            return Ok(result);
        }

        [HttpPut]
        [Route("api/days/{id:guid}", Name = "ChangeDay")]
        public async Task<IHttpActionResult> ChangeDay(Guid id, ChangeDayCommand command)
        {
            if (command == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden) { Content = new StringContent("Missing Command Argument"), ReasonPhrase = "Missing Command Argument" };
                throw new HttpResponseException(response);
            }

            Validate(command);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await dayService.ChangeDayAsync(id, command);

            return Ok(result);
        }


        [HttpPost]
        [Route("api/days/{id:guid}/costitems", Name = "AddCostItem")]
        public async Task<IHttpActionResult> AddCostItem(Guid id, AddCostItemToDayCommand cmd)
        {
            if (cmd == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden) { Content = new StringContent("Missing Command Argument"), ReasonPhrase = "Missing Command Argument" };
                throw new HttpResponseException(response);
            }

            Validate(cmd);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await dayService.AddCostItemToDayAsync(id, cmd);

            return Ok(result);
        }


        [HttpPut]
        [Route("api/itineraries/{id:guid}/reordercostitems", Name = "ReorderCostItems")]
        public async Task<IHttpActionResult> ReorderCostItems(Guid id, ReorderCostItemsForDayCommand cmd)
        {
            if (cmd == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden) { Content = new StringContent("Missing Command Argument"), ReasonPhrase = "Missing Command Argument" };
                throw new HttpResponseException(response);
            }

            Validate(cmd);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await dayService.ReorderCostItemsForDayAsync(id, cmd);

            return Ok(result);
        }

        [HttpDelete]
        [Route("api/itineraries/{id:guid}/costitems/{costitemid:guid}", Name = "RemoveCostItem")]
        public async Task<IHttpActionResult> RemoveCostItem(Guid id, Guid costitemid)
        {
            int version;

            if (!TryGetVersionFromHeader(out version))
            {
                return StatusCode(HttpStatusCode.PreconditionFailed);
            }

            var cmd = new RemoveCostItemFromDayCommand() { CostItemId = costitemid, Version = version };
            var result = await dayService.RemoveCostItemFromDayAsync(id, cmd);

            return Ok(result);
        }
    }
}
