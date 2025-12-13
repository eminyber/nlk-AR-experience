using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.EventBus.EventData.Input;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Interfaces.Listeners;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Listeners
{
    public class ARPlaneTappedListener : MonoBehaviour, IEventListener<ARPlaneTappedEventData>
    {
        [SerializeField] MonoBehaviour eventHandlerReference;
        private IEventHandler<ARPlaneTappedEventData> _eventHandler;

        void OnEnable()
        {
            EventBus.Register<ARPlaneTappedEventData>(this);
        }

        void Start()
        {
            if (eventHandlerReference == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing IEventHandler<T> reference in {nameof(ARPlaneTappedListener)}");
                enabled = false;
                return;
            }

            if (eventHandlerReference is not IEventHandler<ARPlaneTappedEventData>)
            {
                Logger.Log(LogSeverityLevel.Error, $"The referenced IEventHandler<T> is not if type IEventHandler<ARPlaneTappedEventData> in {nameof(ARPlaneTappedListener)}");
                enabled = false;
                return;
            }

            _eventHandler = (IEventHandler<ARPlaneTappedEventData>)eventHandlerReference;
        }

        void OnDisable()
        {
            EventBus.Unregister<ARPlaneTappedEventData>(this);
        }

        public void OnEvent(ARPlaneTappedEventData eventData)
        {
            _eventHandler.HandleEvent(eventData);
        }
    }
}