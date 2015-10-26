using System;

namespace UserInterface.CommandResponses
{
    /// <summary>
    /// WebGateway Response to CreateItineraryCommand: POST api/itineraries
    /// </summary>
    public class CreateItineraryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SeasonId { get; set; }
        public Guid ProductId { get; set; }
        public Guid TourcodeId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid BrandId { get; set; }
        public Guid OperatorId { get; set; }
    }
}