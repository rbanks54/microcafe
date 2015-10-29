using System;
using System.Collections.Generic;
using System.Web.Http;
using Admin.Common.Dto;
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

        [HttpGet]
        [Route("api/products/exists/{name}")]
        public IHttpActionResult Exists(string name)
        {
            var view = ServiceLocator.ProductView;
            //TODO: Fix this
            //return Ok(view.CodeExists(name));
            return Ok(true);
        }
    }
}
