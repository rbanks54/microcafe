using System;
using System.Linq;
using UserInterface.Models.Entities;
using System.Threading.Tasks;
using UserInterface.Gateway.Admin.Commands.Products;
using UserInterface.Gateway.Admin.CommandResponses.Products;

namespace UserInterface.Helpers.Services
{
    public interface IProductsService
    {
        Task<IQueryable<ProductDto>> GetProductListAsync();

        Task<ProductDto> GetProductAsync(Guid id);

        Task<CreateProductResponse> CreateProductAsync(CreateProductCommand cmd);

        Task<AlterProductResponse> AlterProductAsync(Guid id, AlterProductCommand cmd);
    }
}
