using System;

namespace UserInterface.Commands
{
    /// <summary>
    /// ApiGateway Command Parameters for POST api/itineraries
    /// </summary>
    public class CreateItineraryCommand
    {
        public string Name { get; set; }
        public Guid SeasonId { get; set; }
        public Guid TourcodeId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid BrandId { get; set; }
        public Guid OperatorId { get; set; }
        public Guid ProductId { get; set; }
    }
}