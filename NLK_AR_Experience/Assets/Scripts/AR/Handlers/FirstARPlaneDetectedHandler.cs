using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.EventBus.EventData.AR;
using NLKARExperience.Core.Models;

using UnityEngine;

using UnityEngine.XR.ARFoundation;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Handlers
{
    public class FirstARPlaneDetectedHandler : MonoBehaviour
    {
        [SerializeField] ARPlaneManager PlaneManager;

        void OnEnable()
        {
            if (PlaneManager != null)
            {
                PlaneManager.trackablesChanged.AddListener(OnHandleARPlaneChange);
            }  
        }

        void Start()
        {
            if (PlaneManager != null) return;

            Logger.Log(LogSeverityLevel.Error, $"Validation Failed: ARPlaneManager reference is missing in '{nameof(FirstARPlaneDetectedHandler)}'");
            enabled = false;
        }

        void OnDisable()
        {
            if (PlaneManager != null)
            {
                PlaneManager.trackablesChanged.RemoveListener(OnHandleARPlaneChange);
            }
        }

        private void OnHandleARPlaneChange(ARTrackablesChangedEventArgs<ARPlane> changes)
        {
            foreach (ARPlane plane in changes.added)
            {
                EventBus.Publish<ARPlaneDetectionStartedEventData>(new ARPlaneDetectionStartedEventData());
                enabled = false;
                return;
            }
        }
    }
}

