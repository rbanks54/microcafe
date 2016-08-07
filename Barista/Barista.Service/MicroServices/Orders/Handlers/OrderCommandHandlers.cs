using System;
using Barista.Common.Dto;
using Barista.Service.MicroServices.Products.Commands;
using MicroServices.Common.Repository;

namespace Barista.Service.MicroServices.Products.Handlers
{
    public class ProductCommandHandlers
    {
        private readonly IRepository repository;

        public ProductCommandHandlers(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(CreateProduct message)
        {
            // Validation 
            ProductDto existingProduct = null;
            
            // Process
            var series = new Products.Domain.Product(message.Id, message.Code, message.Name, message.Description, message.ProductType);
            repository.Save(series);
        }

        public void Handle(AlterProduct message)
        {
            var product = repository.GetById<Products.Domain.Product>(message.Id);

            int committedVersion = message.OriginalVersion;

            if (!String.Equals(product.Code, message.NewCode, StringComparison.OrdinalIgnoreCase))
            {
                product.ChangeCode(message.NewCode, committedVersion++);
            }

            if (!String.Equals(product.Name, message.NewTitle, StringComparison.OrdinalIgnoreCase))
            {
                product.ChangeTitle(message.NewTitle, committedVersion++);
            }

            if (!String.Equals(product.Description, message.NewDescription, StringComparison.OrdinalIgnoreCase))
            {
                product.ChangeDescription(message.NewDescription, committedVersion++);
            }

            if (message.NewProductType != product.Type)
            {
                product.ChangeType(message.NewProductType, committedVersion);
            }

            repository.Save(product);
        }
    }
}
