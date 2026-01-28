using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.EventBus.EventData.System;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Interfaces.Listeners;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Utils;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.System.Listeners
{
    public class TurnOffARInSceneListener : MonoBehaviour, IEventListener<TurnOffARInSceneRequestedEventData>
    {
        [SerializeField] MonoBehaviour eventHandlerReference;

        private IEventHandler<TurnOffARInSceneRequestedEventData> _eventHandler;

        void OnEnable()
        {
            EventBus.Register<TurnOffARInSceneRequestedEventData>(this);
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
            EventBus.Unregister<TurnOffARInSceneRequestedEventData>(this);
        }

        public void OnEvent(TurnOffARInSceneRequestedEventData eventData)
        {
            _eventHandler.HandleEvent(eventData);
        }

        private bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<IEventHandler<TurnOffARInSceneRequestedEventData>>(eventHandlerReference, out _eventHandler))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(eventHandlerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IEventHandler<TurnOffARInSceneRequestedEventData>' in {nameof(TurnOffARInSceneListener)}");
                return false;
            }

            return true;
        }
    }
}

