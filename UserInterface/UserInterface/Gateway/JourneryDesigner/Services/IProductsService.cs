using System;
using System.Linq;
using UserInterface.Models.Entities;
using System.Threading.Tasks;
using UserInterface.Commands;
using UserInterface.CommandResponses;
using UserInterface.Models.CommandResponses;
using UserInterface.Models.JourneryDesigner.Commands;

namespace UserInterface.Helpers.Services
{
    public interface IProductsService
    {
        Task<IQueryable<ProductDto>> GetProductListAsync();

        Task<ProductDto> GetProductAsync(Guid id);

        Task<CreateProductResponse> CreateProductAsync(CreateProductCommand cmd);

        Task<AlterProductResponse> AlterProductAsync(Guid id, AlterProductCommand cmd);
        Task<bool> ProductExists(string code);
    }
}
