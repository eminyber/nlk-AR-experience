using System;

namespace NLKARExperience.Core.EventSystem.EventTypes
{
    public class SimpleEvent
    {
        private event Action action = delegate { };

        public void RaiseEvent()
        {
            action?.Invoke();
        }

        public void AddListener(Action listener)
        {
            action += listener;
        }

        public void RemoveListener(Action listener)
        {
            action -= listener;
        }

    }
}