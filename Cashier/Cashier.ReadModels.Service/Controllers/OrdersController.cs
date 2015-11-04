using System;
using System.Web.Http;
using MicroServices.Common.Exceptions;

namespace Cashier.ReadModels.Service.Controllers
{
    public class OrdersController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            var view = ServiceLocator.BrandView;
            try
            {
                var dto = view.GetById(id);
                return Ok(dto);
            }
            catch (ReadModelNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var view = ServiceLocator.BrandView;
            var result = view.GetAll();
            return Ok(result);
        }
    }
}