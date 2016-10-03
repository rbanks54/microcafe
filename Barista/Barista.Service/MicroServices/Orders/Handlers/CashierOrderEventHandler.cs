using System;
using Barista.Service.MicroServices.Orders.Commands;
using MicroServices.Common.Repository;
using Barista.Service.MicroServices.Orders.Domain;
using MicroServices.Common;
using Cashier.Common.Events;
using System.Collections.Generic;

namespace Barista.Service.MicroServices.Orders.Handlers
{
    public class CashierOrderEventHandler : ReadModelAggregate,
        IHandle<OrderPlaced>
    {
        private readonly IRepository repository;

        public CashierOrderEventHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(OrderPlaced @event)
        {
            //Events from external sources that need to act on the domain should 
            //cause commands on the domain to be fired.
            var order = new Order(@event.Id, @event.ProductId, @event.Quantity);
            repository.Save(order);
        }
    }
}
