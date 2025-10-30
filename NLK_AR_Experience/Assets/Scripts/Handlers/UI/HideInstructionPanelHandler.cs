using NLKARExperience.Core.EventSystem;
using NLKARExperience.Util.Enums;

using UnityEngine;

using Logger = NLKARExperience.Util.Logger;

namespace NLKARExperience.Handlers
{
    /// <summary>
    /// Destorys the instruction panel when the first ARPlane has been detected
    /// </summary>
    /// <remarks>
    /// This handler uses <see cref="EventManager"/> for listening when the first AR plane
    /// has been detected within the scene and then destroys the instruction panel represented 
    /// as <see cref="GameObject"/>
    /// <para>It requires a UI panel in the form of a <see cref="GameObject"/>.</para>
    /// </remarks>
    public class HideInstructionPanelHandler : MonoBehaviour
    {
        /// <summary>
        /// Cached reference to the UI panel.
        /// </summary>
        [SerializeField] GameObject _instructionPanel;

        /// <summary>
        /// Subscribes to the OnFirstARPlaneDetected event.
        /// </summary>
        void OnEnable()
        {
            EventManager.AppEvent.AR.OnFirstARPlaneDetected.AddListener(onFirstARPlaneDetected);   
        }

        /// <summary>
        /// Validates that the UI panel <see cref="GameObject"/> dependency exists.
        /// </summary>
        /// <remarks>
        /// If the dependency is missing, this component will log an error and disable itself to 
        /// prevent null reference exceptions. 
        /// </remarks>
        void Start()
        {
            if (_instructionPanel != null) return;

            Logger.LogMessage(LogSeverityLevel.Error, $"Missing GameObject reference on {nameof(HideInstructionPanelHandler)}");
            enabled = false;
        }

        /// <summary>
        /// Unsubscribes to the OnFirstARPlaneDetected event.
        /// </summary>
        void OnDisable()
        {
            EventManager.AppEvent.AR.OnFirstARPlaneDetected.RemoveListener(onFirstARPlaneDetected);
        }

        /// <summary>
        /// Callback handler for the <see cref="EventManager.AppEvent.AR.OnFirstARPlaneDetected"/>. It will destroy the 
        /// UI panel cached in the <see cref="GameObject"/> and then disable itself. 
        /// </summary>
        private void onFirstARPlaneDetected()
        {
            Destroy(_instructionPanel);

            //This script should be placed on the UI Panel object but in case this is not the case,
            // disable the script to avoid unwanted behaviours.
            enabled = false;
        }
    }
}