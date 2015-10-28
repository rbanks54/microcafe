using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserInterface.MasterData;

namespace UserInterface.Helpers.Services.MasterData
{
    public interface IOperatorService
    {
        Task<IQueryable<OperatorDto>> GetOperatorListAsync();
        Task<OperatorDto> GetOperatorAsync(Guid id);
        Task<CreateOperatorResponse> CreateOperatorAsync(CreateOperatorCommand cmd);
        Task<AlterOperatorResponse> AlterOperatorAsync(Guid id, AlterOperatorCommand cmd);
        Task<ArchiveOperatorResponse> ArchiveOperatorAsync(Guid id, ArchiveOperatorCommand cmd);
        Task<DeleteOperatorResponse> DeleteOperatorAsync(Guid id, DeleteOperatorCommand cmd);
    }
}
