using System;
using System.Web.Http;
using MicroServices.Common.Exceptions;

namespace Admin.ReadModels.Service.Controllers
{
    public class ProductsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            var view = ServiceLocator.ProductView;
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
            var view = ServiceLocator.ProductView;
            var result = view.GetAll();
            return Ok(result);
        }
    }
}
