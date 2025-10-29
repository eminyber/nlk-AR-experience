using NLKARExperience.Core.EventSystem;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.UI.Util;
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
    /// an event when a plane is hit. It also utilizes <see cref="UIElementDetector"/> to ensure that the 
    /// touch did not occur on a UI element.
    /// </remarks>
    /// <para>It requires an <see cref="ARRaycastManager"/> and an <see cref="UIElementDetector"/> within the scene.</para>
    public class ARPlaneTouchToPoseHandler : MonoBehaviour, IUserInputHandler
    {
        /// <summary>
        /// Cached reference to the <see cref="ARRaycastManager"/> within the scene for performing AR plane raycasts. 
        /// </summary>
        [SerializeField] ARRaycastManager _arRaycastManager;


        /// <summary>
        /// Cached reference to the <see cref="UIElementDetector"/> within the scene for detecting if a touch occurred over
        /// a UI element.
        /// </summary>
        [SerializeField] UIElementDetector _UIElementDetector;

        /// <summary>
        /// Internal list used to store raycast hit results. 
        /// </summary>
        private List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();

        /// <summary>
        /// Validates that the <see cref="ARRaycastManager"/> and <see cref="UIElementDetector"/> exists.
        /// </summary>
        /// <remarks>
        /// If one is missing, this component will log an error and disable itself to prevent null reference
        /// exceptions. 
        /// </remarks>
        void Start()
        {
            if (_arRaycastManager == null)
            {
                Logger.LogMessage(LogSeverityLevel.Error, $"Missing ARRaycastManager reference in {nameof(ARPlaneTouchToPoseHandler)}.");
                enabled = false;
                return;
            }

            if (_UIElementDetector == null) 
            {
                Logger.LogMessage(LogSeverityLevel.Error, $"Missing UIElementDetector reference in {nameof(ARPlaneTouchToPoseHandler)}.");
                enabled = false;
                return;
            }
        }

        /// <summary>
        /// Checks if an AR plane exists at the user's touch position
        /// </summary>
        /// <param name="screenTouchPosition">The position of the user's touch</param>
        public void HandleUserTouchedScreen(Vector2 screenTouchPosition)
        {
            if (!enabled) return;

            if (_UIElementDetector.IsUIElementAtPosition(screenTouchPosition)) return;

            if (_arRaycastManager.Raycast(screenTouchPosition, _raycastHits, TrackableType.PlaneWithinPolygon))
            {
                ARRaycastHit firstARPlaneHit = _raycastHits[0];
                EventManager.InputEvent.Touch.OnPlacementPoseSelected.RaiseEvent(firstARPlaneHit.pose);
            }
        }
    }
}