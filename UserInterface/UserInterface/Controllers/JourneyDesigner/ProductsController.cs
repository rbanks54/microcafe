using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserInterface.Data;
using UserInterface.Helpers.Configuration;
using UserInterface.Helpers.Services;
using UserInterface.Commands;
using UserInterface.CommandResponses;
using System.Web;
using UserInterface.Models.Entities;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using UserInterface.Helpers.ActionResults;
using UserInterface.Models.JourneryDesigner.Commands;

namespace UserInterface.Controllers
{
    /// <summary>
    /// This controller acts as the API Gateway point for the Aggregate "Product"
    /// 
    /// NOTE:
    /// The implementation is design to reflect the methodology of using
    /// a web ui to call this endpoint and then this to communicate to 
    /// the various "Domain Conexts" apis.
    /// 
    /// NOTE:
    /// This is using a repository to dummy up the data interactions.
    /// This will be replaced with api calls to interact with the
    /// services.
    /// </summary>
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

        [HttpGet]
        [Route("api/journeydesigner/products", Name = "GetAllProducts")]
        public async Task<IHttpActionResult> Get(int limit = 10, int offset = 0)
        {
            IQueryable<ProductDto> query;

            var tours = await productsService.GetProductListAsync();
            query = tours.OrderByDescending(b => b.Id);

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / limit);

            var results = query
                         .Skip(offset)
                         .Take(limit)
                         .ToList();

            var response = new PagedOkResult<List<ProductDto>>(results, this)
            {
                TotalCount = totalCount,
                TotalPages = totalPages
            };

            return response;
        }

        [HttpGet]
        [Route("api/journeydesigner/products/{id:guid}", Name = "GetProductById")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var result = await productsService.GetProductAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("api/journeydesigner/products/exists/{code}", Name = "CheckProductExists")]
        public async Task<IHttpActionResult> Exists(string code)
        {
            var result = await productsService.ProductExists(code);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/journeydesigner/products", Name = "CreateProduct")]
        public async Task<IHttpActionResult> CreateProduct(CreateProductCommand cmd)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await productsService.CreateProductAsync(cmd);
            return CreatedAtRoute("GetProductById", new {id = result.Id}, result);            
        }

        [HttpPut]
        [Route("api/journeydesigner/products/{id:guid}", Name = "AlterProduct")]
        public async Task<IHttpActionResult> AlterProduct(Guid id, AlterProductCommand cmd)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await productsService.AlterProductAsync(id, cmd);

            return Ok(result);
        }

        [HttpGet]
        [Route("api/journeydesigner/products/types", Name = "GetProductTypes")]
        public IHttpActionResult GetProductTypes()
        {
            var result = EnumHelper.GetSelectList(typeof (ProductType));
            return Ok(result);
        } 

    }
}
