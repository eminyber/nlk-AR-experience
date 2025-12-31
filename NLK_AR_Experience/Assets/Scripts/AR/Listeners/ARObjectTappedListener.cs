using NLKARExperience.Core.Utils;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Interfaces.Listeners;
using NLKARExperience.Core.EventBus.EventData.Input;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Listeners
{
    public class ARObjectTappedListener : MonoBehaviour, IEventListener<InteractableObjectTappedEventData>
    {
        [SerializeField] MonoBehaviour eventHandlerReference;

        private IEventHandler<InteractableObjectTappedEventData> _eventHandler;

        void OnEnable()
        {
            EventBus.Register<InteractableObjectTappedEventData>(this);
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
            EventBus.Unregister<InteractableObjectTappedEventData>(this);
        }

        public void OnEvent(InteractableObjectTappedEventData eventData)
        {
            _eventHandler.HandleEvent(eventData);
        }

        private bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<IEventHandler<InteractableObjectTappedEventData>>(eventHandlerReference, out _eventHandler))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(eventHandlerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IEventHandler<InteractableObjectTappedEventData>' in {nameof(ARObjectTappedListener)}");
                return false;
            }

            return true;
        }
    }
}