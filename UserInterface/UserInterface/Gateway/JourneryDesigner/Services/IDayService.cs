using System;
using System.Linq;
using System.Threading.Tasks;
using UserInterface.CommandResponses;
using UserInterface.Commands;
using UserInterface.Models;
using UserInterface.Models.Commands;
using UserInterface.Models.Entities;
using UserInterface.Data;

namespace UserInterface.Helpers.Services
{
    public interface IDayService
    {
        Task<IQueryable<DayDto>> GetDayListAsync();

        Task<DayDto> GetDayAsync(Guid id);

        Task<CreateDayResponse> CreateDayAsync(CreateDayCommand command);
        Task<ChangeDayResponse> ChangeDayAsync(Guid id, ChangeDayCommand command);
        Task<DeleteDayResponse> DeleteDayAsync(Guid id, DeleteDayCommand command);

        Task<AddCostItemToDayResponse> AddCostItemToDayAsync(Guid id, AddCostItemToDayCommand cmd);
        Task<RemoveCostItemFromDayResponse> RemoveCostItemFromDayAsync(Guid id, RemoveCostItemFromDayCommand cmd);
        Task<ReorderCostItemsForDayResponse> ReorderCostItemsForDayAsync(Guid id, ReorderCostItemsForDayCommand cmd);
    }
}
