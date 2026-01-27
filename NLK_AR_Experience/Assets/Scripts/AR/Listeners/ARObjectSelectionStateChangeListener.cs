using NLKARExperience.Core.Utils;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Interfaces.Listeners;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Listeners
{
    public class ARObjectSelectionStateChangeListener : MonoBehaviour, IEventListener<ARObjectSelectionStateChangedEventData>
    {
        [SerializeField] MonoBehaviour eventHandlerReference;

        private IEventHandler<ARObjectSelectionStateChangedEventData> _eventHandler;

        void OnEnable()
        {
            EventBus.Register<ARObjectSelectionStateChangedEventData>(this);
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
            EventBus.Unregister<ARObjectSelectionStateChangedEventData>(this);
        }

        public void OnEvent(ARObjectSelectionStateChangedEventData eventData)
        {
            _eventHandler.HandleEvent(eventData);
        }

        private bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<IEventHandler<ARObjectSelectionStateChangedEventData>>(eventHandlerReference, out _eventHandler))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(eventHandlerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IEventHandler<ARObjectSelectionStateChangedEventData>' in {nameof(ARObjectSelectionStateChangeListener)}");
                return false;
            }

            return true;
        }
    }
}