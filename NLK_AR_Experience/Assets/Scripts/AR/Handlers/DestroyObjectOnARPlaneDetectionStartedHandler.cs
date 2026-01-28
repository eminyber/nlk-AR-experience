using NLKARExperience.Core.EventBus.EventData.AR;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Handlers
{
    public class DestroyObjectOnARPlaneDetectionStartedHandler : MonoBehaviour, IEventHandler<ARPlaneDetectionStartedEventData>
    {
        [SerializeField] GameObject ObjectToDestroyReference;

        void Start()
        {
            if (ObjectToDestroyReference != null) return;

            Logger.Log(LogSeverityLevel.Error, $"Error: ObjectToDestroyReference is null in {nameof(DestroyObjectOnARPlaneDetectionStartedHandler)}");
            enabled = false;

        }

 
        public void HandleEvent(ARPlaneDetectionStartedEventData eventData)
        {
            if (!enabled) return;

            Destroy(ObjectToDestroyReference);
        }
    }
}