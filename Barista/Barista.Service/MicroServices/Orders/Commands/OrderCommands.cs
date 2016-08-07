using System;
using Barista.Common.Enums;
using MicroServices.Common;

namespace Barista.Service.MicroServices.Products.Commands
{
    public class CreateProduct : ICommand
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ProductType ProductType { get; set; }

        public CreateProduct(Guid id, string code, string name, string description, ProductType productType)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
            ProductType = productType;
        }
    }

    public class AlterProduct : ICommand
    {
        public Guid Id { get; private set; }
        public string NewCode { get; private set; }
        public string NewTitle { get; set; }
        public string NewDescription { get; set; }
        public ProductType NewProductType { get; set; }
        public int OriginalVersion { get; private set; }

        public AlterProduct(Guid id, int originalVersion, string newCode, string newTitle, string newDescription, ProductType newProductType)
        {
            Id = id;
            NewCode = newCode;
            NewTitle = newTitle;
            NewDescription = newDescription;
            NewProductType = newProductType;
            OriginalVersion = originalVersion;
        }
    }
}
