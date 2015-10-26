using System;
using Barista.Common.Clients;
using Barista.Common.Enums;
using Barista.Common.Events;
using MicroServices.Common;

namespace Barista.Service.MicroServices.Products.Domain
{
    public class Product : Aggregate
    {
        private Product() { }

        public Product(Guid id, string name, string title, string description, ProductType type)
        {
            ValidateCode(name);

            ApplyEvent(new ProductCreated(id, name, title, description, type));
        }

        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ProductType Type { get; set; }
        
        private void Apply(ProductCreated e)
        {
            Id = e.Id;
            Code = e.Code;
            Name = e.Name;
            Description = e.Description;
        }

        private void Apply(ProductCodeChanged e)
        {
            Code = e.NewCode;
        }

        private void Apply(ProductTypeChanged e)
        {
            Type = e.NewProductType;
        }

        private void Apply(ProductDescriptionChanged e)
        {
            Description = e.NewDescription;
        }

        private void Apply(ProductNameChanged e)
        {
            Name = e.NewName;
        }

        public void ChangeCode(string newCode, int originalVersion)
        {
            ValidateCode(newCode);
            ValidateVersion(originalVersion);

            ApplyEvent(new ProductCodeChanged(Id, newCode));
        }

        public void ChangeTitle(string newTitle, int originalVersion)
        {
            ValidateVersion(originalVersion);

            ApplyEvent(new ProductNameChanged(Id, newTitle));
        }

        public void ChangeDescription(string newDescription, int originalVersion)
        {
            ValidateVersion(originalVersion);

            ApplyEvent(new ProductDescriptionChanged(Id, newDescription));
        }

        public void ChangeType(ProductType newType, int originalVersion)
        {
            ValidateVersion(originalVersion);

            ApplyEvent(new ProductTypeChanged(Id, newType));
        }

        void ValidateCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException("Invalid code specified: cannot be empty.", "code");
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
