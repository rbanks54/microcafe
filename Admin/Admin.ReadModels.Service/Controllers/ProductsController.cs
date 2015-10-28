using System;
using System.Collections.Generic;
using System.Web.Http;
using Admin.Common.Dto;
using Admin.Common.Enums;
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

                LoadAssociatedObjects(dto);

                return Ok(dto);
            }
            catch (ReadModelNotFoundException)
            {
                return NotFound();
            }
        }

        private void LoadAssociatedObjects(IEnumerable<ProductDto> products)
        {
            foreach (var product in products)
            {
                LoadAssociatedObjects(product);
            }
        }

        private void LoadAssociatedObjects(ProductDto dto)
        {
            // Load the Associated objects
            dto.ProductTypeDisplayName = System.Text.RegularExpressions.Regex.Replace(
                  dto.ProductType.ToString(),
                  "([^^])([A-Z])",
                  "$1 $2"
                );

            dto.DisplayName = string.Format("{0} ({1})", dto.Name, dto.Code);
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var view = ServiceLocator.ProductView;
            var result = view.GetAll();
            LoadAssociatedObjects(result);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/products/exists/{code}")]
        public IHttpActionResult Exists(string code)
        {
            var view = ServiceLocator.ProductView;
            return Ok(view.CodeExists(code));
            
        }
    }
}
