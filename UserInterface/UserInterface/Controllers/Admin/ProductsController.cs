using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using UserInterface.Gateway.Admin.Commands.Products;
using UserInterface.Helpers.Services;


namespace UserInterface.Controllers
{
    public class ProductsController : ApiController
    {
        private IProductsService productsService;

        public ProductsController() : this(new ProductsService())
        {
        }

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [Route("api/admin/products")]
        public async Task<IHttpActionResult> Get(int limit = 1000, int offset = 0)
        {
            var products = (await productsService.GetProductListAsync()).ToList();
            var response = Ok(products);

            return response;
        }

        [Route("api/admin/product/{id:guid}",Name ="GetProductById")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var result = await productsService.GetProductAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("api/admin/products")]
        public async Task<IHttpActionResult> Post(CreateProductCommand cmd)
        {
            var result = await productsService.CreateProductAsync(cmd);

            return CreatedAtRoute("GetProductById", new { id = result.Id }, result);
        }

        [Route("api/admin/product/{id:guid}")]
        public async Task<IHttpActionResult> Put(Guid id, AlterProductCommand cmd)
        {
            var result = await productsService.AlterProductAsync(id, cmd);

            return Ok(result);
        }
    }
}
