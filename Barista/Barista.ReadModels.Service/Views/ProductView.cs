using System;
using System.Collections.Generic;
using Barista.Common.Dto;
using MicroServices.Common;
using Barista.Common.Events;
using System.Collections.Concurrent;
using System.Linq;
using MicroServices.Common.Exceptions;
using MicroServices.Common.Repository;

namespace Barista.ReadModels.Service.Views
{
    public class ProductView : ReadModelAggregate,
        IHandle<ProductCreated>,
        IHandle<ProductCodeChanged>,
        IHandle<ProductDescriptionChanged>,
        IHandle<ProductNameChanged>,
        IHandle<ProductTypeChanged>
    {
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
                throw new ReadModelNotFoundException(id, typeof(OperatorDto));
            }
        }

        public IEnumerable<ProductDto> GetAll()
        {
            return repository.GetAll();
        }

        public bool CodeExists(string code)
        {
            //TODO: Allowing duplicates for now. Need this to work off a different view classs
            return false;
            //return products.Values.Any(p => p.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
        }

        public void Apply(ProductCreated e)
        {
            var dto = new ProductDto
            {
                Id = e.Id,
                Code = e.Code,
                Name = e.Name,
                Description = e.Description,
                ProductType = e.ProductType,
                Version = e.Version
            };
            repository.Insert(dto);
        }
        public void Apply(ProductCodeChanged e)
        {
            var product = GetById(e.Id);
            product.Code = e.NewCode;
            product.Version = e.Version;
        }
        public void Apply(ProductNameChanged e)
        {
            var product = GetById(e.Id);
            product.Name = e.NewName;
            product.Version = e.Version;
            repository.Update(product);
        }
        public void Apply(ProductDescriptionChanged e)
        {
            var product = GetById(e.Id);
            product.Description = e.NewDescription;
            product.Version = e.Version;
            repository.Update(product);
        }
        public void Apply(ProductTypeChanged e)
        {
            var product = GetById(e.Id);
            product.ProductType = e.NewProductType;
            product.Version = e.Version;
            repository.Update(product);
        }

        public void Add(ProductCreated e, int version)
        {
            ApplyEvent(e, version);
        }
        public void AlterCode(ProductCodeChanged e, int version)
        {
            ApplyEvent(e, version);
        }
        public void AlterTitle(ProductNameChanged e, int version)
        {
            ApplyEvent(e, version);
        }
        public void AlterDescription(ProductDescriptionChanged e, int version)
        {
            ApplyEvent(e, version);
        }
        public void AlterType(ProductTypeChanged e, int version)
        {
            ApplyEvent(e, version);
        }
    }
}