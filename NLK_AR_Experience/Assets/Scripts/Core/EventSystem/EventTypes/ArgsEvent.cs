using System;

namespace NLKARExperience.Core.EventSystem.EventTypes
{
    public class ArgsEvent<T> where T : EventArgs
    {
        private event EventHandler<T> action = delegate { };

        public void RaiseEvent(object sender, T eventArg)
        {
            action?.Invoke(sender, eventArg);
        }

        public void AddListener(EventHandler<T> listener)
        {
            action += listener;
        }

        public void RemoveListener(EventHandler<T> listener)
        {
            action -= listener;
        }
    }
}