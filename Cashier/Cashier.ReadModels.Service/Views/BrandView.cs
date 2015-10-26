using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Cashier.Common.Dto;
using Cashier.Common.Events;
using MicroServices.Common;
using MicroServices.Common.Exceptions;
using MicroServices.Common.Repository;
using StackExchange.Redis;

namespace Cashier.ReadModels.Service.Views
{
    public class BrandView : ReadModelAggregate,
        IHandle<BrandCreated>,
        IHandle<BrandNameChanged>,
        IHandle<BrandCodeChanged>
    {
        private readonly IReadModelRepository<BrandDto> repository;

        public BrandView(IReadModelRepository<BrandDto> repository)
        {
            this.repository = repository;
        }

        public BrandDto GetById(Guid id)
        {
            try
            {
                return repository.Get(id);
            }
            catch
            {
                throw new ReadModelNotFoundException(id, typeof(BrandDto));
            }
        }

        public IEnumerable<BrandDto> GetAll()
        {
            return repository.GetAll();
        }

        public void Apply(BrandCreated e)
        {
            var dto = new BrandDto { Id = e.Id, Code = e.Code, Name = e.Name, Version = e.Version };
            repository.Insert(dto);
  
        }

        public void Apply(BrandCodeChanged e)
        {
            var brand = GetById(e.Id);
            brand.Code = e.NewCode;
            brand.Version = e.Version;
            repository.Update(brand);
        }

        public void Apply(BrandNameChanged e)
        {
            var brand = GetById(e.Id);
            brand.Name = e.NewName;
            brand.Version = e.Version;
            repository.Update(brand);
        }

        public void Add(BrandCreated e, int version)
        {
            ApplyEvent(e, version);
        }
        
        public void AlterCode(BrandCodeChanged e, int version)
        {
            ApplyEvent(e, version);
        }

        public void AlterTitle(BrandNameChanged e, int version)
        {
            ApplyEvent(e, version);
        }
    }
}