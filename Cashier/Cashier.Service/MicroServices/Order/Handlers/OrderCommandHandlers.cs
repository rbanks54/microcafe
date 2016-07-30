using System;
using Cashier.Service.MicroServices.Order.Commands;
using MicroServices.Common.Repository;

namespace Cashier.Service.MicroServices.Order.Handlers
{
    public class OrderCommandHandlers
    {
        private readonly IRepository repository;

        public OrderCommandHandlers(IRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(StartNewOrder message)
        {
            var order = new Domain.Order(message.Id, message.ProductId, message.Quantity);
            repository.Save(order);
        }

        public void Handle(PayForOrder message)
        {
            var order = repository.GetById<Domain.Order>(message.Id);
            int committableVersion = message.Version;
            order.PayForOrder(committableVersion);
            repository.Save(order);
        }

        void ValidateProduct(Guid productId)
        {
            if (productId != Guid.Empty)
            {
                try
                {
                    //productClient.GetById(productId);
                }
                catch (Exception)
                {
                    throw new ArgumentOutOfRangeException("productId", "Invalid product identifier specified: the product cannot be found.");
                }
            }
        }

    }
}