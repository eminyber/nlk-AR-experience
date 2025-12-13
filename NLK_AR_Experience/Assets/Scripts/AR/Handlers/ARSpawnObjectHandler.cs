using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.EventBus.EventData.Input;
using NLKARExperience.Core.Interfaces.Systems;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Handlers
{
    public class ARSpawnObjectHandler : MonoBehaviour, IEventHandler<ARPlaneTappedEventData>
    {
        [SerializeField] MonoBehaviour arSpawnObjectSystemReference;
        ISpawnSystem _arSpawnSystem; 

        void Start()
        {
            if (arSpawnObjectSystemReference == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing ISpawnSystem reference in {nameof(ARSpawnObjectHandler)}");
                enabled = false;
                return;
            }

            if (arSpawnObjectSystemReference is not ISpawnSystem)
            {
                Logger.Log(LogSeverityLevel.Error, $"The referenced ISpawnSystem is not of this type in {nameof(ARSpawnObjectHandler)}");
                enabled = false;
                return;
            }

            _arSpawnSystem = (ISpawnSystem) arSpawnObjectSystemReference;
        }

        public void HandleEvent(ARPlaneTappedEventData eventData)
        {
            if (!enabled) return;

            _arSpawnSystem.SpawnObject(eventData.Pose);
        }
    }
}