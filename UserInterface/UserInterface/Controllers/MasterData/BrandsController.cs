using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using UserInterface.Helpers.ActionResults;
using UserInterface.Helpers.Services.MasterData;
using UserInterface.MasterData;


namespace UserInterface.Controllers
{
    public class BrandsController : ApiController
    {
        private IBrandService brandService;

        public BrandsController() : this(new BrandService())
        {
        }

        public BrandsController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        [HttpGet]
        [Route("api/masterdata/brands", Name = "GetAllBrands")]
        public async Task<IHttpActionResult> Get(int limit = 1000, int offset = 0)
        {
            IQueryable<BrandDto> query;

            var brands = await brandService.GetBrandListAsync();
            query = brands.OrderByDescending(b => b.Code);

            var totalCount = brands.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / limit);

            var results = query
                         .Skip(offset)
                         .Take(limit)
                         .ToList();

            var response = new PagedOkResult<List<BrandDto>>(results, this)
            {
                TotalCount = totalCount,
                TotalPages = totalPages
            };

            return response;
        }

        [HttpGet]
        [Route("api/masterdata/brands/{id:guid}", Name = "GetbrandById")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var result = await brandService.GetBrandAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("api/masterdata/brands", Name = "CreateBrand")]
        public async Task<IHttpActionResult> CreateBrand(CreateBrandCommand cmd)
        {
            var result = await brandService.CreateBrandAsync(cmd);

            return CreatedAtRoute("GetBrandById", new { id = result.Id }, result);
        }

        [HttpPut]
        [Route("api/masterdata/brands/{id:guid}", Name = "AlterBrand")]
        public async Task<IHttpActionResult> AlterBrand(Guid id, AlterBrandCommand cmd)
        {
            var result = await brandService.AlterBrandAsync(id, cmd);

            return Ok(result);
        }
    }
}
