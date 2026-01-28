using NLKARExperience.Core.EventBus.EventData.System;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Utils;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Handlers
{
    public class DeleteAllSpawnedObjectsHandler : MonoBehaviour, IEventHandler<ResetCurrentSceneRequestedEventData>
    {
        [SerializeField] MonoBehaviour ARObjectDeletionSystemReference;

        private IDeletionSystem _arObjectDeletionSystem;

        void Start()
        {
            bool validationSucceeded = ValidateScriptDependencies();
            if (!validationSucceeded)
            {
                enabled = false;
            }
        }

        public void HandleEvent(ResetCurrentSceneRequestedEventData eventData)
        {
            if (eventData.Equals(null)) return;

            _arObjectDeletionSystem.DeleteAllObjects();
        }

        private bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<IDeletionSystem>(ARObjectDeletionSystemReference, out _arObjectDeletionSystem))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(ARObjectDeletionSystemReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IDeletionSystem' in {nameof(DeleteAllSpawnedObjectsHandler)}");
                return false;
            }

            return true;
        }
    }
}