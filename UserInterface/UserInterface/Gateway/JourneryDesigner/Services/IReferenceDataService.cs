using System;
using System.Linq;
using System.Threading.Tasks;
using UserInterface.Models.Entities;

namespace UserInterface.Helpers.Services
{
    public interface IReferenceDataService
    {
        Task<IQueryable<BrandDto>> GetBrandListAsync();
        Task<BrandDto> GetBrandAsync(Guid id);
        Task<IQueryable<OwnerDto>> GetOwnerListAsync();
        Task<OwnerDto> GetOwnerAsync(Guid id);
        Task<IQueryable<OperatorDto>> GetOperatorListAsync();
        Task<OperatorDto> GetOperatorAsync(Guid id);
        Task<IQueryable<SeasonDto>> GetSeasonListAsync();
        Task<SeasonDto> GetSeasonAsync(Guid id);
        Task<IQueryable<ProductDto>> GetProductListAsync();
        Task<ProductDto> GetProductAsync(Guid id);
    }
}
