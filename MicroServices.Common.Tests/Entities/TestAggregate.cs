namespace MicroServices.Common.Tests.Entities
{
    public class TestAggregate : Aggregate
    {
        public TestAggregate()
        {
            ApplyEvent(new TestAggregateCreated());
        }

        public void DoChange()
        {
            ApplyEvent(new TestAggregateCreated());
        }
    }

}
