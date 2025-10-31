using NLKARExperience.Core.EventSystem;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Util.Enums;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

using Logger = NLKARExperience.Util.Logger;

namespace NLKARExperience.Handlers
{
    /// <summary>
    /// Handles user touch input to detect and respond to touches on detected AR planes.
    /// </summary>
    /// <remarks>
    /// This handler uses <see cref="ARRaycastManager"/> to perform raycasts agains AR planes and triggers
    /// an event when a plane is hit. 
    /// </remarks>
    /// <para>It requires an <see cref="ARRaycastManager"/> to exist within the scene.</para>
    public class ARPlaneTouchToPoseHandler : MonoBehaviour, IUserInputHandler
    {
        /// <summary>
        /// Cached reference to the <see cref="ARRaycastManager"/> within the scene for performing AR plane raycasts. 
        /// </summary>
        [SerializeField] ARRaycastManager _arRaycastManager;


        /// <summary>
        /// Internal list used to store raycast hit results. 
        /// </summary>
        private List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();

        /// <summary>
        /// Validates that the <see cref="ARRaycastManager"/> exists.
        /// </summary>
        /// <remarks>
        /// If it is missing, this component will log an error and disable itself to prevent null reference
        /// exceptions. 
        /// </remarks>
        void Start()
        {
            if (_arRaycastManager != null) return;

            Logger.LogMessage(LogSeverityLevel.Error, $"Missing ARRaycastManager reference in {nameof(ARPlaneTouchToPoseHandler)}.");
            enabled = false;
            return;
        }

        /// <summary>
        /// Checks if an AR plane exists at the user's touch position
        /// </summary>
        /// <param name="screenTouchPosition">The position of the user's touch</param>
        public bool HandleUserTouchedScreen(Vector2 screenTouchPosition)
        {
            if (!enabled) return false;

            if (_arRaycastManager.Raycast(screenTouchPosition, _raycastHits, TrackableType.PlaneWithinPolygon))
            {
                ARRaycastHit firstARPlaneHit = _raycastHits[0];
                EventManager.InputEvent.Touch.OnPlacementPoseSelected.RaiseEvent(firstARPlaneHit.pose);
                return true;
            }

            return false;
        }
    }
}