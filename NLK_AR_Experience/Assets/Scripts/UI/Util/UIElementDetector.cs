using NLKARExperience.Util.Enums;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Logger = NLKARExperience.Util.Logger;

namespace NLKARExperience.UI.Util
{
    /// <summary>
    /// Detects if a UI element is located at a specific screen position. 
    /// </summary>
    /// <remarks>>
    /// This class utilizes the <see cref="GraphicRaycaster"/> and <see cref="EventSystem"/> to 
    /// determine if a UI element is located at a specific screen/touch position.
    /// <para>
    /// It requires an <see cref="GraphicRaycaster"/> and an <see cref="EventSystem"/>.
    /// </para>
    /// </remarks>
    public class UIElementDetector : MonoBehaviour
    {
        [Header("UI References")]
        ///<summary>
        ///Cached reference to the graphic raycaster located within the scene.
        /// </summary>
        [SerializeField] private GraphicRaycaster _graphicRaycaster;

        /// <summary>
        /// Cached reference to the event system located within the scene. 
        /// </summary>
        [SerializeField] private EventSystem _eventSystem;


        /// <summary>
        /// Tries to retrive the <see cref="GraphicRaycaster"/> and <see cref="EventSystem"/> from the scene if they
        /// have not been defined.
        /// </summary>
        void OnEnable()
        {
            if (_graphicRaycaster == null)
                _graphicRaycaster = GetComponent<GraphicRaycaster>();

            if (_eventSystem == null)
                _eventSystem = GetComponent<EventSystem>();
        }

        /// <summary>
        /// Validates that the <see cref="GraphicRaycaster"/> and <see cref="EventSystem"/> dependencies exists.
        /// </summary>
        /// <remarks>
        /// If any of the dependent components are missing, this component will log an error and disable itself
        /// to prevent null reference exceptions. 
        /// </remarks>
        void Start()
        {
            if (_graphicRaycaster == null)
            {
                Logger.LogMessage(LogSeverityLevel.Error, $"Missing GraphicRaycaster reference on {nameof(UIElementDetector)}.");
                enabled = false;
                return;
            }

            if (_eventSystem == null)
            {
                Logger.LogMessage(LogSeverityLevel.Error, $"Missing EventSystem reference on {nameof(UIElementDetector)}.");
                enabled = false;
                return;
            }
        }

        /// <summary>
        /// Checks if any UI element (graphics) exists at a specific screen position.
        /// </summary>
        /// <param name="position">The screen position at which to check for an UI element</param>
        /// <returns>
        /// <c>true</c> if a UI element is found at the position, otherwise <c>false</c>.
        /// </returns>
        public bool IsUIElementAtPosition(Vector2 position)
        {
            if (!enabled) return false;

            if (_eventSystem.IsPointerOverGameObject()) return false;

            var newPointerEventData = new PointerEventData(_eventSystem);
            newPointerEventData.position = position;

            var raycastResults = new List<RaycastResult>();
            _graphicRaycaster.Raycast(newPointerEventData, raycastResults);

            return raycastResults.Count > 0;
        }
    }
}