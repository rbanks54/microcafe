using System;
using Newtonsoft.Json;
using MicroServices.Common;

namespace Admin.Common.Events
{
    public class ProductCreated : Event
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public ProductCreated(Guid id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
        [JsonConstructor]
        private ProductCreated(Guid id, string name, string description, decimal price, int version)
            : this(id, name, description, price)
        {
            Version = version;
        }
    }

    public class ProductNameChanged : Event
    {
        public ProductNameChanged(Guid id, string newName)
        {
            Id = id;
            NewName = newName;
        }

        [JsonConstructor]
        private ProductNameChanged(Guid id, string newName, int version) : this(id, newName)
        {
            Version = version;
        }

        public string NewName { get; private set; }
    }

    public class ProductDescriptionChanged : Event
    {
        public ProductDescriptionChanged(Guid id, string newDescription)
        {
            Id = id;
            NewDescription = newDescription;
        }

        [JsonConstructor]
        private ProductDescriptionChanged(Guid id, string newDescription, int version) : this(id, newDescription)
        {
            Version = version;
        }

        public string NewDescription { get; private set; }
    }

    public class ProductPriceChanged : Event
    {
        public ProductPriceChanged(Guid id, decimal newPrice)
        {
            Id = id;
            NewPrice = newPrice;
        }

        [JsonConstructor]
        private ProductPriceChanged(Guid id, decimal newPrice, int version)
            : this(id, newPrice)
        {
            Version = version;
        }

        public decimal NewPrice { get; private set; }
    }
}
