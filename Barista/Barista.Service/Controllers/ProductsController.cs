using System;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using Barista.Service.DataTransferObjects.Commands;
using Barista.Service.MicroServices.Products.Commands;
using Barista.Service.MicroServices.Products.Handlers;
using MicroServices.Common.Exceptions;

namespace Barista.Service.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly ProductCommandHandlers handler;

        public ProductsController()
            : this(ServiceLocator.ProductCommands)
        {}

        public ProductsController(ProductCommandHandlers handler)
        {
            this.handler = handler;
        }


        [HttpPost]
        public IHttpActionResult Post(CreateProductCommand cmd)
        {
            // TODO: is this to be handled here or in the Aggregate?
            if (string.IsNullOrWhiteSpace(cmd.Code))
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("code must be supplied in the body"),
                    ReasonPhrase = "Missing product code"
                };
                throw new HttpResponseException(response);
            }
            
            try
            {
                var command = new CreateProduct(Guid.NewGuid(), cmd.Code, cmd.Name, cmd.Description, cmd.ProductType);
                handler.Handle(command);

                var link = new Uri(string.Format("http://localhost:8181/api/products/{0}", command.Id));
                return Created<CreateProduct>(link, command);
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

        [HttpPut]
        [Route("api/products/{id:guid}")]
        public IHttpActionResult Put(Guid id, AlterProductCommand cmd)
        {
            // TODO: is this to be handled here or in the Aggregate?
            if (string.IsNullOrWhiteSpace(cmd.Code))
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("code must be supplied in the body"),
                    ReasonPhrase = "Missing product code"
                };
                throw new HttpResponseException(response);
            }
            
            try
            {
                var command = new AlterProduct(id, cmd.Version, cmd.Code, cmd.Name, cmd.Description, cmd.ProductType);
                handler.Handle(command);

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
