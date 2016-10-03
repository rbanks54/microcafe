using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cashier.Service.DataTransferObjects.Commands;
using Cashier.Service.MicroServices.Order.Commands;
using MicroServices.Common.Exceptions;

namespace Cashier.Service.Controllers
{
    public class OrdersController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post(PlaceOrderCommand cmd)
        {
            if (Guid.Empty.Equals(cmd.Id))
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden) {
                    Content = new StringContent("order information must be supplied in the POST body"),
                    ReasonPhrase = "Missing Order Id"
                };
                throw new HttpResponseException(response);
            }

            var command = new StartNewOrder(cmd.Id, cmd.ProductId, cmd.Quantity);

            try
            {
                ServiceLocator.OrderCommands.Handle(command);

                var link = new Uri(string.Format("http://localhost:8182/api/orders/{0}", command.Id));
                return Created(link, command);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
            }
        }
    }
}