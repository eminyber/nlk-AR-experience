using NLKARExperience.Core.EventSystem;
using NLKARExperience.Util.Enums;

using UnityEngine;
using UnityEngine.XR.ARFoundation;

using Logger = NLKARExperience.Util.Logger;

namespace NLKARExperience.Listeners
{
    /// <summary>
    /// Listens for the first ARPlane detected
    /// </summary>
    /// <remarks>
    /// This class uses AR Foundation's <see cref="ARPlaneManager"/> to detect when the first
    /// ARPlane has been detected in the Scene. <see cref="EventManager"/> sends an event upon 
    /// the first detected ARPlane to tell other parts of the system when this happens. 
    /// <para>
    /// It requires an <see cref="ARPlaneManager"/> component to live within the scene.
    /// </para>
    /// </remarks>
    public class FirstARPlaneDetector : MonoBehaviour
    {
        /// <summary>
        /// Cached reference to the ARPlaneManager.
        /// </summary>
        [SerializeField] ARPlaneManager _planeManager;

        /// <summary>
        /// Subscribes to <see cref="ARPlaneManager"/>'s trackableChanged event. 
        /// </summary>
        /// <remarks>
        /// If the <see cref="ARPlaneManager"/> is missing then nothing is done. 
        /// </remarks>
        void OnEnable()
        {
            if (_planeManager != null)
                _planeManager.trackablesChanged.AddListener(onPlaneChanged);
        }

        /// <summary>
        /// Validates that the <see cref="EventManager"/> dependency exists.
        /// </summary>
        /// <remarks>
        /// If the <see cref="ARPlaneManager"/> is missing, this component will log an error and
        /// disable itself to prevent null reference exceptions. 
        /// </remarks>
        void Start()
        {
            if (_planeManager == null)
            {
                Logger.LogMessage(LogSeverityLevel.Error, $"Missing ARPlaneManager reference on {nameof(FirstARPlaneDetector)}");
                enabled = false;
                return;
            }
        }

        /// <summary>
        /// Unsubscribes from <see cref="ARPlaneManager"/>'s trackableChanged event. 
        /// </summary>
        void OnDisable()
        {
            if (_planeManager != null)
                _planeManager.trackablesChanged.RemoveListener(onPlaneChanged);
        }

        /// <summary>
        /// Callback handler for the <see cref="_planeManager.trackablesChanged"/> event.
        /// </summary>
        /// <remarks>
        /// This method will determine when the first ARPlane has been added to the scene and
        /// send an event via the <see cref="EventManager"/> to tell it's listeners that it has 
        /// happened. Once the first ARPlane has been detected, the component disables itself. 
        /// </remarks>
        /// <param name="changes">The ARPlane changes that has happend since the last update</param>
        private void onPlaneChanged(ARTrackablesChangedEventArgs<ARPlane> changes)
        {
            if (changes == null) return;

            if (changes.added == null) return;

            EventManager.AppEvent.AR.OnFirstARPlaneDetected.RaiseEvent();
            enabled = false;
        }
    }
}