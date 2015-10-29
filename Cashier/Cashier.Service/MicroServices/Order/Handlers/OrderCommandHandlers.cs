using System;
using Cashier.Service.MicroServices.Brand.Commands;
using MicroServices.Common.Repository;

namespace Cashier.Service.MicroServices.Brand.Handlers
{
    public class OrderCommandHandlers
    {
        private readonly IRepository repository;

        public OrderCommandHandlers(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(PlaceOrder message)
        {
            var brand = new Domain.Order(message.Id, message.Code, message.Name);
            repository.Save(brand);
        }
    }
}