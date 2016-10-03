using Barista.Service.MicroServices.Orders.Commands;
using Barista.Service.MicroServices.Orders.Domain;
using Barista.Service.MicroServices.Orders.Handlers;
using Cashier.Common.Events;
using MicroServices.Common;
using MicroServices.Common.MessageBus;
using MicroServices.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Barista.Service.Tests
{
    public class OrderCommandTests
    {
        private readonly IMessageBus bus;
        private readonly IRepository repository;

        public OrderCommandTests()
        {
            bus = new InProcessMessageBus();
            repository = new InMemoryRepository(bus);
        }

        [Fact]
        public void Should_raise_prepared_event_when_barista_completes_preparation()
        {
            var id = Guid.NewGuid();
            var handler = new BaristaOrderCommandHandlers(repository);
            var e = new OrderPlaced(id, Guid.NewGuid(), 1);
            var eventHandler = new CashierOrderEventHandler(repository);
            eventHandler.Apply(e);

            var order = repository.GetById<BaristaOrder>(id);
            Assert.False(order.IsCompleted);

            var command = new CompleteOrder(id, order.Version);
            handler.Handle(command);

            order = repository.GetById<BaristaOrder>(id);
            Assert.True(order.IsCompleted);
        }
    }
}
