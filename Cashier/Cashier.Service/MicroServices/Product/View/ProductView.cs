using MicroServices.Common.General.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cashier.Service.MicroServices.Product.Domain;

namespace Cashier.Service.MicroServices.Product.View
{
    public class ProductView
    {
        private readonly Admin.ReadModels.Client.IProductsView adminProducts;
        private static ConcurrentDictionary<Guid, Domain.Product> products = new ConcurrentDictionary<Guid, Domain.Product>();

        public ProductView(Admin.ReadModels.Client.IProductsView adminProductView)
        {
            adminProducts = adminProductView;
            InitialiseProducts();
        }

        public ProductView() : this(new Admin.ReadModels.Client.ProductsView()) { }

        public IEnumerable<Domain.Product> GetAll()
        {
            return products.Values.AsEnumerable();
        }

        private void InitialiseProducts()
        {
            var transformedDtos = adminProducts.GetProducts().Select(p => new Domain.Product
            {
                Id = p.Id,
                Price = p.Price
            });
            foreach (var p in transformedDtos)
            {
                products.TryAdd(p.Id, p);
            }
        }

        internal void Add(Guid id, decimal price)
        {
            var success = products.TryAdd(id, new Domain.Product() { Id = id, Price = price });
            if (!success)
            {
                //Assume product is already present from a previous event (not sure if it's possible)
                //So we'll just update the price instead.
                var p = GetById(id);
                p.Price = price;
            }
        }

        public Domain.Product GetById(Guid id)
        {
            Domain.Product product;
            if (products.TryGetValue(id, out product)) return product;

            throw new ArgumentOutOfRangeException("id","A product with the id of " + id.ToString() + "couldn't be found");
        }
    }
}
