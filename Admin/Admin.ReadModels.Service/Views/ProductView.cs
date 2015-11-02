using System;
using System.Collections.Generic;
using Admin.Common.Dto;
using MicroServices.Common;
using Admin.Common.Events;
using System.Collections.Concurrent;
using System.Linq;
using MicroServices.Common.Exceptions;
using MicroServices.Common.Repository;

namespace Admin.ReadModels.Service.Views
{
    public class ProductView : ReadModelAggregate,
        IHandle<ProductCreated>,
        IHandle<ProductDescriptionChanged>,
        IHandle<ProductNameChanged>,
        IHandle<ProductPriceChanged>
    {
        private const string displayFormat = "{1} ({0})";
        private readonly IReadModelRepository<ProductDto> repository;

        public ProductView(IReadModelRepository<ProductDto> repository)
        {
            this.repository = repository;
        }

        public ProductDto GetById(Guid id)
        {
            try
            {
                return repository.Get(id);
            }
            catch
            {
                throw new ReadModelNotFoundException(id, typeof(ProductDto));
            }
        }

        public IEnumerable<ProductDto> GetAll()
        {
            return repository.GetAll();
        }

        public void Apply(ProductCreated e)
        {
            var dto = new ProductDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Price = e.Price,
                Version = e.Version,
                DisplayName = string.Format(displayFormat, e.Name, e.Description),
            };
            repository.Insert(dto);
        }
        public void Apply(ProductNameChanged e)
        {
            var product = GetById(e.Id);
            product.Name = e.NewName;
            product.Version = e.Version;
            product.DisplayName = string.Format(displayFormat, product.Name, product.Description);
            repository.Update(product);
        }
        public void Apply(ProductDescriptionChanged e)
        {
            var product = GetById(e.Id);
            product.Description = e.NewDescription;
            product.Version = e.Version;
            product.DisplayName = string.Format(displayFormat, product.Name, product.Description);
            repository.Update(product);
        }
        public void Apply(ProductPriceChanged e)
        {
            var product = GetById(e.Id);
            product.Price = e.NewPrice;
            product.Version = e.Version;
            repository.Update(product);
        }
    }
}