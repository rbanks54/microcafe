using System;
using MicroServices.Common;
using Admin.Common.Events;
using System.Collections.Generic;
using System.Linq;
using MicroServices.Common.Exceptions;

namespace Barista.Service.CachedData.Products
{
    public class Products : ReadModelAggregate,
        IHandle<ProductCreated>,
        IHandle<ProductNameChanged>
    {
        private readonly IList<Product> cache;

        public Products(IList<Product> cache)
        {
            this.cache = cache;
        }

        public Product GetById(Guid id)
        {
            try
            {
                return cache.First(p => p.Id.Equals(id));
            }
            catch
            {
                throw new ReadModelNotFoundException(id, typeof(Product));
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return cache.AsEnumerable();
        }

        public void Apply(ProductCreated e)
        {
            var dto = new Product
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
            };
            cache.Add(dto);
        }

        public void Apply(ProductNameChanged e)
        {
            var product = GetById(e.Id);
            product.Name = e.NewName;
        }

        public void Apply(ProductDescriptionChanged e)
        {
            var product = GetById(e.Id);
            product.Description = e.NewDescription;
        }

        public void Add(ProductCreated e, int version)
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
    }
}
