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
using Admin.ReadModels.Client;

namespace Cashier.ReadModels.Service.Views
{
    public class OrderView : ReadModelAggregate,
        IHandle<OrderPlaced>,
        IHandle<OrderPaidFor>
    {
        private readonly IReadModelRepository<OrderDto> repository;

        public OrderView(IReadModelRepository<OrderDto> repository)
        {
            this.repository = repository;
        }

        public OrderDto GetById(Guid id)
        {
            try
            {
                return repository.Get(id);
            }
            catch
            {
                throw new ReadModelNotFoundException(id, typeof(OrderDto));
            }
        }

        public IEnumerable<OrderDto> GetAll()
        {
            return repository.GetAll();
        }

        public void Apply(OrderPlaced e)
        {
            var productView = new ProductsView();
            var product = productView.GetById(e.ProductId);
            var dto = new OrderDto
            {
                Id = e.Id,
                Quantity = e.Quantity,
                ProductName = product.Description,
                Version = e.Version,
                IsPaidFor = false
            };
            repository.Insert(dto);
        }

        public void Apply(OrderPaidFor e)
        {
            var order = GetById(e.Id);
            order.Version = e.Version;
            order.IsPaidFor = true;
            repository.Update(order);
        }
    }
}