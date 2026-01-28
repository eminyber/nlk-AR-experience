using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.EventBus.EventData.AR;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Interfaces.Listeners;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Utils;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Listeners
{
    public class ARPlaneDetectionStartedListener : MonoBehaviour, IEventListener<ARPlaneDetectionStartedEventData>
    {
        [SerializeField] MonoBehaviour eventHandlerReference;

        private IEventHandler<ARPlaneDetectionStartedEventData> _eventHandler;

        void OnEnable()
        {
            EventBus.Register<ARPlaneDetectionStartedEventData>(this);
        }

        void Start()
        {
            bool validationSucceeded = ValidateScriptDependencies();
            if (!validationSucceeded)
            {
                enabled = false;
            }
        }

        void OnDisable()
        {
            EventBus.Unregister<ARPlaneDetectionStartedEventData>(this);
        }

        public void OnEvent(ARPlaneDetectionStartedEventData eventData)
        {
            _eventHandler.HandleEvent(eventData);
        }

        private bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<IEventHandler<ARPlaneDetectionStartedEventData>>(eventHandlerReference, out _eventHandler))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(eventHandlerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IEventHandler<ARPlaneDetectionStartedEventData>' in {nameof(ARPlaneDetectionStartedListener)}");
                return false;
            }

            return true;
        }
    }
}