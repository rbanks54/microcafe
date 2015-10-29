using System;
using Admin.Common.Events;
using MicroServices.Common;

namespace Admin.Service.MicroServices.Products.Domain
{
    public class Product : Aggregate
    {
        private Product() { }

        public Product(Guid id, string name, string description, decimal price)
        {
            ValidateName(name);

            ApplyEvent(new ProductCreated(id, name, description, price));
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; set; }

        private void Apply(ProductCreated e)
        {
            Id = e.Id;
            Name = e.Name;
            Description = e.Description;
            Price = e.Price;
        }

        private void Apply(ProductPriceChanged e)
        {
            Price = e.NewPrice;
        }

        private void Apply(ProductDescriptionChanged e)
        {
            Description = e.NewDescription;
        }

        private void Apply(ProductNameChanged e)
        {
            Name = e.NewName;
        }

        public void ChangeName(string newName, int originalVersion)
        {
            ValidateName(newName);
            ValidateVersion(originalVersion);

            ApplyEvent(new ProductNameChanged(Id, newName));
        }

        public void ChangeDescription(string newDescription, int originalVersion)
        {
            ValidateVersion(originalVersion);

            ApplyEvent(new ProductDescriptionChanged(Id, newDescription));
        }

        public void ChangePrice(decimal newPrice, int originalVersion)
        {
            ValidateVersion(originalVersion);

            ApplyEvent(new ProductPriceChanged(Id, newPrice));
        }

        void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid name specified: cannot be empty.", "name");
            }
        }

        void ValidateVersion(int version)
        {
            if (Version != version)
            {
                throw new ArgumentOutOfRangeException("version", "Invalid version specified: the version is out of sync.");
            }
        }
    }
}
