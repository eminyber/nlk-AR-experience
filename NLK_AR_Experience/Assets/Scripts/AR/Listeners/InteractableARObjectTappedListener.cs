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
    public class InteractableARObjectTappedListener : MonoBehaviour, IEventListener<InteractableTappedEventData>
    {
        [SerializeField] MonoBehaviour eventHandlerReference;

        private IEventHandler<InteractableTappedEventData> _eventHandler;

        void OnEnable()
        {
            EventBus.Register<InteractableTappedEventData>(this);
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
            EventBus.Unregister<InteractableTappedEventData>(this);
        }

        public void OnEvent(InteractableTappedEventData eventData)
        {
            _eventHandler.HandleEvent(eventData);
        }

        private bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<IEventHandler<InteractableTappedEventData>>(eventHandlerReference, out _eventHandler))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(eventHandlerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IEventHandler<InteractableObjectTappedEventData>' in {nameof(InteractableARObjectTappedListener)}");
                return false;
            }

            return true;
        }
    }
}