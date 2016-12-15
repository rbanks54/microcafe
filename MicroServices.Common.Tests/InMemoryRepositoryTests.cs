using System.Linq;
using MicroServices.Common.MessageBus;
using MicroServices.Common.Repository;
using MicroServices.Common.Tests.Entities;
using Moq;
using NUnit.Framework;

namespace MicroServices.Common.Tests
{
    public class InMemoryRepositoryTests
    {
        [Test]
        public void Test_That_InMemoryRepository__Pushes_Events_To_Bus()
        {
            IMessageBus messageBus = Mock.Of<IMessageBus>();
            InMemoryRepository repository = new InMemoryRepository(messageBus);
            TestAggregate testAggregate = new TestAggregate();

            repository.Save(testAggregate);
            Mock.Get(messageBus).Verify(x => x.Publish(It.IsAny<Event>()), Times.Exactly(1));
        }

        [Test]
        public void Test_That_InMemoryRepository_Doesnt_Push_Duplicate_Events()
        {
            IMessageBus messageBus = Mock.Of<IMessageBus>();
            InMemoryRepository repository = new InMemoryRepository(messageBus);
            TestAggregate aggregate = new TestAggregate();
            aggregate.DoChange();
            repository.Save(aggregate);

            aggregate.DoChange();
            repository.Save(aggregate);

            aggregate.DoChange();
            repository.Save(aggregate);

            Mock.Get(messageBus).Verify(x => x.Publish(It.IsAny<Event>()), Times.Exactly(repository.GetLatestEvents().Count()));
        }
    }
}
