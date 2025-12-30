using NLKARExperience.Core.Utils;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Interfaces.Systems;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.EventBus.EventData.Input;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Handlers
{
    public class SpawnObjectOnARPlaneTappedHandler : MonoBehaviour, IEventHandler<ARPlaneTappedEventData>
    {
        [SerializeField] MonoBehaviour arSpawnObjectSystemReference;

        ISpawnSystem _arSpawnSystem; 

        void Start()
        {
            bool validationSucceeded = ValidateScriptDependencies();
            if (!validationSucceeded)
            {
                enabled = false;
            }
        }

        public void HandleEvent(ARPlaneTappedEventData eventData)
        {
            if (!enabled) return;

            _arSpawnSystem.SpawnObject(eventData.Pose);
        }

        private bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<ISpawnSystem>(arSpawnObjectSystemReference, out _arSpawnSystem))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(arSpawnObjectSystemReference)}' does not implement or contain required dependency " +
                                                   $"of type 'ISpawnSystem' in {nameof(SpawnObjectOnARPlaneTappedHandler)}");
                return false;
            }

            return true;
        }
    }
}