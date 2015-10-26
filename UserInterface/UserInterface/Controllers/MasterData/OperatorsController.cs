using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UserInterface.Commands;
using UserInterface.Helpers.ActionResults;
using UserInterface.Helpers.Services;
using UserInterface.Helpers.Services.MasterData;
using UserInterface.MasterData;
using UserInterface.Helpers.Exceptions;

namespace UserInterface.Controllers
{
    public class OperatorsController : ApiController
    {
        private IOperatorService operatorService;

        public OperatorsController() : this(new OperatorService())
        {
        }

        public OperatorsController(IOperatorService operatorService)
        {
            this.operatorService = operatorService;
        }

        [HttpGet]
        [Route("api/masterdata/operators", Name = "GetAllOperators")]
        public async Task<IHttpActionResult> Get()
        {
            var operators = await operatorService.GetOperatorListAsync();
            IQueryable<OperatorDto> query = operators.OrderByDescending(b => b.Code);

            return Ok<IQueryable<OperatorDto>>(query);
        }

        [HttpGet]
        [Route("api/masterdata/operators/{id:guid}", Name = "GetOperatorById")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            OperatorDto result = null;

            try
            {
                result = await operatorService.GetOperatorAsync(id);
            }
            catch (AggregateException ex)
            {
                if (ex.InnerExceptions.Any(e => e is DtoNotFoundException))
                    return NotFound();

                throw;
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("api/masterdata/operators", Name = "CreateOperator")]
        public async Task<IHttpActionResult> CreateOperator(CreateOperatorCommand cmd)
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

            var result = await operatorService.CreateOperatorAsync(cmd);

            return CreatedAtRoute("GetOperatorById", new { id = result.Id }, result);
        }

        [HttpPut]
        [Route("api/masterdata/operators/{id:guid}", Name = "AlterOperator")]
        public async Task<IHttpActionResult> AlterOperator(Guid id, AlterOperatorCommand cmd)
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

            var result = await operatorService.AlterOperatorAsync(id, cmd);

            return Ok(result);
        }

        [HttpPut]
        [Route("api/masterdata/operators/{id:guid}/archive", Name = "ArchiveOperator")]
        public async Task<IHttpActionResult> AlterOperator(Guid id, ArchiveOperatorCommand cmd)
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

            var result = await operatorService.ArchiveOperatorAsync(id, cmd);

            return Ok(result);
        }

        [HttpDelete]
        [Route("api/masterdata/operators/{id:guid}", Name = "DeleteOperator")]
        public async Task<IHttpActionResult> DeleteOperator(Guid id)
        {
            int version;

            if (!this.TryGetVersionFromHeader(out version))
            {
                return StatusCode(HttpStatusCode.PreconditionFailed);
            }

            DeleteOperatorCommand cmd = new DeleteOperatorCommand() { Version = version };
            var result = await operatorService.DeleteOperatorAsync(id, cmd);

            return Ok(result);
        }
    }
}
