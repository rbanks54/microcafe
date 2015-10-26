namespace MicroServices.Common
{
    public interface IHandle<in T> where T:Event
    {
    }
}