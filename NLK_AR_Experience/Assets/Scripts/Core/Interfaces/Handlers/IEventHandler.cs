namespace NLKARExperience.Core.Interfaces.Handlers
{
    public interface IEventHandler{}

    public interface IEventHandler<T> : IEventHandler
    {
        public void HandleEvent(T eventData);
    }
}