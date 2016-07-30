using MicroServices.Common.General.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier.Service.MicroServices.Product.View
{
    public interface IProductView
    {
        IEnumerable<Domain.Product> GetAll();
    }

    public class ProductView : IProductView
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

    }
}
