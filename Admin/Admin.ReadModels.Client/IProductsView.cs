using System;
using System.Collections.Generic;
using Admin.Common.Dto;

namespace Admin.ReadModels.Client
{
    public interface IProductsView
    {
        ProductDto GetById(Guid id);
        IEnumerable<ProductDto> GetProducts();
        void Initialise();
        void Reset();
        void UpdateLocalCache(ProductDto newValue);
    }
}