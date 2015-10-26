using System;

namespace UserInterface.Models.Commands
{
    public class ChangeItineraryCommand
    {
        public string Name { get; set; }
        public Guid SeasonId { get; set; }
        public Guid TourcodeId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid BrandId { get; set; }
        public Guid OperatorId { get; set; }
        public Guid ProductId { get; set; }
        public int Version { get; set; }
    }
}
