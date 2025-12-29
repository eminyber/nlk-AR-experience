using NLKARExperience.Core.Interfaces.Handlers.Input;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.EventBus.EventData.Input;
using NLKARExperience.Core.EventBus;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.Input.Handlers
{
    public class ARPlaneInteractionHandler : MonoBehaviour, IUserInputHandler
    {
        [SerializeField] ARRaycastManager arRaycastManager;

        private List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();

        void Start()
        {
            if (arRaycastManager != null) return;

            Logger.Log(LogSeverityLevel.Error, $"Missing ARRaycastManager reference in {nameof(ARPlaneInteractionHandler)}.");
            enabled = false;
            return;
        }

        public bool ProcessInput(Vector2 screenTouchPosition)
        {
            if (!enabled) return false;

            if (arRaycastManager.Raycast(screenTouchPosition, _raycastHits, TrackableType.PlaneWithinPolygon))
            {
                ARRaycastHit firstARPlaneHit = _raycastHits[0];
                EventBus.Publish<ARPlaneTappedEventData>(new ARPlaneTappedEventData(firstARPlaneHit.pose));

                return true;
            }

            return false;
        }
    }
}
