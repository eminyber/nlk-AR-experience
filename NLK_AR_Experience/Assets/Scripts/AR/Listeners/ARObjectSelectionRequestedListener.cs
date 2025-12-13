using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.EventBus.EventData.AR;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Interfaces.Listeners;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Listeners
{
    public class ARObjectSelectionRequestedListener : MonoBehaviour, IEventListener<ARObjectToSpawnChangeRequestedEventData>
    {
        [SerializeField] MonoBehaviour eventHandlerReference;
        private IEventHandler<ARObjectToSpawnChangeRequestedEventData> _eventHandler;

        void OnEnable()
        {
            EventBus.Register<ARObjectToSpawnChangeRequestedEventData>(this);
        }

        void Start()
        {
            if (eventHandlerReference == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing IEventHandler<T> reference in {nameof(ARObjectSelectionRequestedListener)}");
                enabled = false;
                return;
            }

            if (eventHandlerReference is not IEventHandler<ARObjectToSpawnChangeRequestedEventData>)
            {
                Logger.Log(LogSeverityLevel.Error, $"The referenced IEventHandler<T> is not of type IEventHandler<ARObjectSelectionRequestedEventData> in {nameof(ARObjectSelectionRequestedListener)}");
                enabled = false;
                return;
            }

            _eventHandler = (IEventHandler<ARObjectToSpawnChangeRequestedEventData>)eventHandlerReference;
        }

        void OnDisable()
        {
            EventBus.Unregister<ARObjectToSpawnChangeRequestedEventData>(this);
        }

        public void OnEvent(ARObjectToSpawnChangeRequestedEventData eventData)
        {
            _eventHandler.HandleEvent(eventData);
        }
    }
}