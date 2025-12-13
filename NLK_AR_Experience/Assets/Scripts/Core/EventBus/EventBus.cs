using NLKARExperience.Core.Interfaces.Listeners;

using System;
using System.Collections.Generic;
using System.Linq;

namespace NLKARExperience.Core.EventBus
{
    public static class EventBus
    {
       
        private static readonly Dictionary<Type, List<object>> _listenerCache = new Dictionary<Type, List<object>>();

        public static void Register<T>(IEventListener<T> eventListener)
        {
            Type eventType = typeof(T);

            if (!_listenerCache.ContainsKey(eventType))
            {
                _listenerCache.Add(eventType, new List<object>());
            }
            
            if (!_listenerCache[eventType].Contains(eventListener))
            {
                _listenerCache[eventType].Add(eventListener);
            }
        }

        public static void Unregister<T>(IEventListener<T> eventListener) 
        { 
            Type eventType = typeof(T);

            if (_listenerCache.ContainsKey(eventType))
            {
                _listenerCache[(eventType)].Remove(eventListener);
            }
        }

        public static void Publish<T>(T eventData)
        {
            Type eventType = typeof(T);

            if (_listenerCache.TryGetValue(eventType, out List<object> eventListeners))
            {
                foreach (var eventListener in eventListeners.ToList())
                {
                    ((IEventListener<T>)eventListener).OnEvent(eventData);
                }
            }
        }
    }
}