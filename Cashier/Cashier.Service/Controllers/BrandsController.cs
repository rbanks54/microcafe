using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cashier.Service.DataTransferObjects.Commands;
using Cashier.Service.MicroServices.Brand.Commands;
using MicroServices.Common.Exceptions;

namespace Cashier.Service.Controllers
{
    public class BrandsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post(CreateBrandCommand cmd)
        {
            if (string.IsNullOrWhiteSpace(cmd.Code))
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden) {Content = new StringContent("code must be supplied in the body"), ReasonPhrase = "Missing Brand Code"};
                throw new HttpResponseException(response);
            }

            var command = new CreateBrand(Guid.NewGuid(), cmd.Code, cmd.Name);

            try
            {
                ServiceLocator.BrandCommands.Handle(command);

                // TODO: need to config up this url
                var link = new Uri(string.Format("http://localhost:8182/api/brands/{0}", command.Id));
                return Created(link, command);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message );
            }
        }

        [HttpPut]
        [Route("api/brands/{id:guid}")]
        public IHttpActionResult Put(Guid id, AlterBrandCommand cmd)
        {
            if (string.IsNullOrWhiteSpace(cmd.Code))
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden) {Content = new StringContent("code must be supplied in the body"), ReasonPhrase = "Missing brand code"};
                throw new HttpResponseException(response);
            }

            var command = new AlterBrand(id, cmd.Version, cmd.Code, cmd.Name);

            try
            {
                ServiceLocator.BrandCommands.Handle(command);
                
                return Ok(command);
            }
            catch (AggregateNotFoundException)
            {
                return NotFound();
            }
            catch (AggregateDeletedException)
            {
                return Conflict();
            }
        }
    }
}