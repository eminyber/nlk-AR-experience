using NLKARExperience.Core.Utils;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Interfaces.Listeners;
using NLKARExperience.Core.EventBus.EventData.System;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.System.Handlers
{
    public class ResetCurrentSceneListener : MonoBehaviour, IEventListener<ResetCurrentSceneRequestedEventData>
    {
        [SerializeField] MonoBehaviour ResetCurrentSceneHandlerReference;

        IEventHandler<ResetCurrentSceneRequestedEventData> _resetCurrentSceneHandler;

        void OnEnable()
        {
            EventBus.Register<ResetCurrentSceneRequestedEventData>(this);
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
            EventBus.Unregister<ResetCurrentSceneRequestedEventData>(this);
        }

        public void OnEvent(ResetCurrentSceneRequestedEventData eventData)
        {
            _resetCurrentSceneHandler.HandleEvent(eventData);
        }

        private bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency <IEventHandler<ResetCurrentSceneRequestedEventData>>(ResetCurrentSceneHandlerReference, out _resetCurrentSceneHandler))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(ResetCurrentSceneHandlerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IEventHandler<ResetCurrentSceneRequestedEventData>' in {nameof(ResetCurrentSceneListener)}");
                return false;
            }

            return true;
        }
    }
}