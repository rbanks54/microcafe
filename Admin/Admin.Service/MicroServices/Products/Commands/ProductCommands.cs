using System;
using MicroServices.Common;

namespace Admin.Service.MicroServices.Products.Commands
{
    public class CreateProduct : ICommand
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; set; }

        public CreateProduct(Guid id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
    }

    public class AlterProduct : ICommand
    {
        public Guid Id { get; private set; }
        public string NewTitle { get; set; }
        public string NewDescription { get; set; }
        public decimal NewPrice { get; set; }
        public int OriginalVersion { get; private set; }

        public AlterProduct(Guid id, int originalVersion, string newTitle, string newDescription, decimal newPrice)
        {
            Id = id;
            NewTitle = newTitle;
            NewDescription = newDescription;
            NewPrice = newPrice;
            OriginalVersion = originalVersion;
        }
    }
}
