using System;
using Newtonsoft.Json;
using Barista.Common.Enums;
using MicroServices.Common;

namespace Barista.Common.Events
{
    public class ProductCreated : Event
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ProductType ProductType { get; private set; }
        public ProductCreated(Guid id, string code, string name, string description, ProductType productType)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
            ProductType = productType;
        }
        [JsonConstructor]
        private ProductCreated(Guid id, string code, string name, string description, ProductType productType, int version): this(id, code, name, description, productType)
        {
            Version = version;
        }
    }

    public class ProductCodeChanged : Event
    {
        public string NewCode { get; private set; }
        public ProductCodeChanged(Guid id, string newCode)
        {
            Id = id;
            NewCode = newCode;
        }

        [JsonConstructor]
        private ProductCodeChanged(Guid id, string newCode, int version): this(id, newCode)
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

    public class ProductTypeChanged : Event
    {
        public ProductTypeChanged(Guid id, ProductType newProductType)
        {
            Id = id;
            NewProductType = newProductType;
        }

        [JsonConstructor]
        private ProductTypeChanged(Guid id, ProductType newProductType, int version) : this(id, newProductType)
        {
            Version = version;
        }

        public ProductType NewProductType { get; private set; }
    }
}
