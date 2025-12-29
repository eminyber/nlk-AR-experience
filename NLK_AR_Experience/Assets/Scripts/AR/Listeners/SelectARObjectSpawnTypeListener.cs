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
    public class SelectARObjectSpawnTypeListener : MonoBehaviour, IEventListener<SelectSpawnTypeRequestEventData>
    {
        [SerializeField] MonoBehaviour eventHandlerReference;

        private IEventHandler<SelectSpawnTypeRequestEventData> _eventHandler;

        void OnEnable()
        {
            EventBus.Register<SelectSpawnTypeRequestEventData>(this);
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
            EventBus.Unregister<SelectSpawnTypeRequestEventData>(this);
        }

        public void OnEvent(SelectSpawnTypeRequestEventData eventData)
        {
            _eventHandler.HandleEvent(eventData);
        }

        private bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<IEventHandler<SelectSpawnTypeRequestEventData>>(eventHandlerReference, out _eventHandler))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(eventHandlerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IEventHandler<ARObjectToSpawnChangeRequestedEventData>' in {nameof(SelectARObjectSpawnTypeListener)}");
                return false;
            }

            return true;
        }
    }
}