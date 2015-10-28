using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.MasterData;

namespace UserInterface.Helpers.Services.MasterData
{
    public interface IBrandService
    {
        Task<IQueryable<BrandDto>> GetBrandListAsync();
        Task<BrandDto> GetBrandAsync(Guid id);
        Task<CreateBrandResponse> CreateBrandAsync(CreateBrandCommand cmd);
        Task<AlterBrandResponse> AlterBrandAsync(Guid id, AlterBrandCommand cmd);
    }
}
