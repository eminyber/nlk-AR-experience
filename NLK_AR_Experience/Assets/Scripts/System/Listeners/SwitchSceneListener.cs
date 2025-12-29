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
    public class SwitchSceneListener : MonoBehaviour, IEventListener<SwitchSceneRequestedEventData>
    {
        [SerializeField] MonoBehaviour sceneSwitchHandlerReference;

        private IEventHandler<SwitchSceneRequestedEventData> _sceneSwitcheventHandler;

        void OnEnable()
        {
            EventBus.Register<SwitchSceneRequestedEventData>(this);
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
            EventBus.Unregister<SwitchSceneRequestedEventData>(this);
        }

        public void OnEvent(SwitchSceneRequestedEventData eventData)
        {
            _sceneSwitcheventHandler.HandleEvent(eventData);
        }

        private bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<IEventHandler<SwitchSceneRequestedEventData>>(sceneSwitchHandlerReference, out _sceneSwitcheventHandler))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(sceneSwitchHandlerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IEventHandler<SwitchSceneRequestedEventData>' in {nameof(SwitchSceneListener)}");
                return false;
            }

            return true;
        }
    }
}