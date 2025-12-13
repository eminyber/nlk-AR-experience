namespace NLKARExperience.Core.Interfaces.Listeners
{
    public interface IEventListener {}

    public interface  IEventListener<T> : IEventListener
    {
        public void OnEvent(T eventData);
    }
}