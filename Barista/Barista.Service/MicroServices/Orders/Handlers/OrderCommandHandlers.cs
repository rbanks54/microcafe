using System;
using Barista.Service.MicroServices.Orders.Commands;
using MicroServices.Common.Repository;
using Barista.Service.MicroServices.Orders.Domain;

namespace Barista.Service.MicroServices.Orders.Handlers
{
    public class OrderCommandHandlers
    {
        private readonly IRepository repository;

        public OrderCommandHandlers(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(CompleteOrder message)
        {
            var order = repository.GetById<Order>(message.Id);

            int committedVersion = message.OriginalVersion;

            order.CompletePreparation(committedVersion++);

            repository.Save(order);
        }
    }
}
