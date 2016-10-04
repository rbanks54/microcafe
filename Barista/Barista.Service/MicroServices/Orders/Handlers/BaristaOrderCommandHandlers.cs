using System;
using Barista.Service.MicroServices.Orders.Commands;
using MicroServices.Common.Repository;
using Barista.Service.MicroServices.Orders.Domain;

namespace Barista.Service.MicroServices.Orders.Handlers
{
    public class BaristaOrderCommandHandlers
    {
        private readonly IRepository repository;

        public BaristaOrderCommandHandlers(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(CompleteOrder message)
        {
            var order = repository.GetById<BaristaOrder>(message.Id);

            int committedVersion = message.OriginalVersion;

            order.CompletePreparation(committedVersion++);

            repository.Save(order);
        }
    }
}
