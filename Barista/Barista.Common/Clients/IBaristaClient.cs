using System;
using System.Collections.Generic;
using Barista.Common.Dto;

namespace Barista.Common.Clients
{
    public interface IBaristaClient
    {
        void Initialise();
        
        void Reset();

        IEnumerable<OperatorDto> GetOperators();
        OperatorDto GetOperatorById(Guid operatorId);
        void DeleteOperator(Guid guid);
        void InsertOrUpdateOperator(OperatorDto newValue);
        
        IEnumerable<BrandDto> GetBrands();
        BrandDto GetBrandById(Guid brandId);
        void InsertOrUpdateBrand(BrandDto newValue);

    }
}