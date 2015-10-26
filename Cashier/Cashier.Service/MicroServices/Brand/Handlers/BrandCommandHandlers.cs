using System;
using Cashier.Service.MicroServices.Brand.Commands;
using MicroServices.Common.Repository;

namespace Cashier.Service.MicroServices.Brand.Handlers
{
    public class BrandCommandHandlers
    {
        private readonly IRepository repository;

        public BrandCommandHandlers(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(CreateBrand message)
        {
            var brand = new Domain.Brand(message.Id, message.Code, message.Name);
            repository.Save(brand);
        }

        public void Handle(AlterBrand message)
        {
            var brand = repository.GetById<Domain.Brand>(message.Id);

            int committedVersion = message.OriginalVersion;

            if(!String.Equals(brand.Code, message.NewCode, StringComparison.OrdinalIgnoreCase))
            {
                brand.ChangeCode(message.NewCode, committedVersion++);
            }

            if (!String.Equals(brand.Name, message.NewName, StringComparison.OrdinalIgnoreCase))
            {
                brand.ChangeName(message.NewName, committedVersion);
            }
            
            repository.Save(brand);
        }
    }
}