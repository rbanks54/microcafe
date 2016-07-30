using Cashier.Service.MicroServices.Order.Commands;
using Cashier.Service.MicroServices.Order.Domain;
using Cashier.Service.MicroServices.Order.Handlers;
using MicroServices.Common.MessageBus;
using MicroServices.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cashier.Service.Tests
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
        public void Should_fail_when_no_product_is_provided_and_a_new_order_is_started()
        {
            var command = new StartNewOrder(Guid.NewGuid(), Guid.Empty, 1);
            var handler = new OrderCommandHandlers(null);
            Assert.Throws<ArgumentNullException>(() => handler.Handle(command));
        }

        [Fact]
        public void Should_fail_when_starting_a_new_order_and_product_quantity_is_zero()
        {
            var command = new StartNewOrder(Guid.NewGuid(), Guid.NewGuid(), 0);
            var handler = new OrderCommandHandlers(null);
            Assert.Throws<ArgumentOutOfRangeException>(() => handler.Handle(command));
        }

        [Fact]
        public void Should_succeed_when_starting_a_new_order_with_a_valid_product_and_quantity()
        {
            var id = Guid.NewGuid();
            //TODO: Create a valid product
            var productId = Guid.NewGuid();

            var command = new StartNewOrder(id, productId, 1);
            var handler = new OrderCommandHandlers(repository);
            handler.Handle(command);

            var order = repository.GetById<Order>(id);
            Assert.Equal(1, order.Quantity);
        }
    }
}
