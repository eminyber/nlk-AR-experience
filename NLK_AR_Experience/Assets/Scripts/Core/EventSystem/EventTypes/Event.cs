using System;

namespace NLKARExperience.Core.EventSystem.EventTypes
{
    public class Event<T>
    {
        private event Action<T> action = delegate { };

        public void RaiseEvent(T parameter)
        {
            action?.Invoke(parameter);
        }

        public void AddListener(Action<T> listener)
        {
            action += listener;
        }

        public void RemoveListener(Action<T> listener)
        {
            action -= listener;
        }
    }
}