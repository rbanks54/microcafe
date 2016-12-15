using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Admin.Service.MicroServices.Products.Domain;
using MicroServices.Common.MessageBus;
using MicroServices.Common.Repository;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;

namespace MicroServices.Common.Tests
{
    public class InMemoryRepositoryTests
    {
        [Test]
        public void Test_That_InMemoryRepository__Pushes_Events_To_Bus()
        {
            IMessageBus messageBus = Mock.Of<IMessageBus>();
            InMemoryRepository repository = new InMemoryRepository(messageBus);
            Product aggregate = new Product(Guid.NewGuid(), "Name", "Description", 2);

            repository.Save(aggregate);
            Mock.Get(messageBus).Verify(x => x.Publish(It.IsAny<Event>()), Times.Exactly(1));
        }

        [Test]
        public void Test_That_InMemoryRepository_Doesnt_Push_Duplicate_Events()
        {
            IMessageBus messageBus = Mock.Of<IMessageBus>();
            InMemoryRepository repository = new InMemoryRepository(messageBus);
            Product aggregate = new Product(Guid.NewGuid(), "Name", "Description", 2);
            aggregate.ChangeName("New Name", 0);
            repository.Save(aggregate);

            aggregate.ChangeName("New Name", 1);
            repository.Save(aggregate);

            aggregate.ChangeName("New Name", 2);
            repository.Save(aggregate);

            Mock.Get(messageBus).Verify(x => x.Publish(It.IsAny<Event>()), Times.Exactly(repository.GetLatestEvents().Count()));
        }
    }
}
