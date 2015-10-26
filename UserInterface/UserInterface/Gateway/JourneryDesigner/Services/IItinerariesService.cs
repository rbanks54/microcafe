using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserInterface.CommandResponses;
using UserInterface.Commands;
using UserInterface.Gateway.JourneryDesigner.Commands.Itinerary;
using UserInterface.Models;
using UserInterface.Models.Commands;
using UserInterface.Models.Entities;

namespace UserInterface.Helpers.Services
{
    public interface IItinerariesService
    {
        Task<IEnumerable<ItineraryDto>> GetItinerariesAsync();

        Task<ItineraryDto> GetItineraryAsync(Guid id);

        Task<CreateItineraryResponse> CreateItineraryAsync(CreateItineraryCommand cmd);
        Task<ChangeItineraryResponse> ChangeItineraryAsync(Guid id, ChangeItineraryCommand command);
        Task<DeleteItineraryResponse> DeleteItineraryAsync(Guid id, DeleteItineraryCommand command);

        Task<AddDayToItineraryResponse> AddDayToItineraryAsync(Guid id, AddDayToItineraryCommand command);
        Task<ReorderDaysForItineraryResponse> ReorderDaysForItineraryAsync(Guid id, ReorderDaysForItineraryCommand command);
        Task<RemoveDayFromItineraryResponse> RemoveDayFromItineraryAsync(Guid id, RemoveDayFromItineraryCommand command);
    }
}
