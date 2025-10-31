using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Interfaces;
using NLKARExperience.Util.Enums;

using UnityEngine;

using Logger = NLKARExperience.Util.Logger;
using NLKARExperience.Core.EventSystem;

namespace NLKARExperience.Handlers
{
    /// <summary>
    /// Detects if a selectable object exist at a given screen position. 
    /// </summary>
    /// <remarks>>
    /// This class utilizes the <see cref="Camera"/>  to 
    /// determine if a selectable object is located at a specific screen/touch position.
    /// <para>
    /// It requires an <see cref="Camera"/> object to exist within the scene.
    /// </para>
    /// </remarks>
    public class ObjectSelectionInputHandler : MonoBehaviour, IUserInputHandler
    {
        /// <summary>
        /// Cached reference to the main camera. 
        /// </summary>
        [SerializeField] Camera _camera;

        /// <summary>
        /// Retrives the main camera if the dependency is not already met.
        /// </summary>
        void OnEnable()
        {
            if (_camera == null) 
                _camera = Camera.main;
        }

        /// <summary>
        /// Validates that the <see cref="Camera"/> dependency is met. 
        /// </summary>
        /// <remarks>
        /// If the main camera reference is missing, this component will log an error and then 
        /// disable itself in order to protect from null reference exceptions. 
        /// </remarks>
        void Start()
        {
            if (_camera != null) return;

            Logger.LogMessage(LogSeverityLevel.Error, $"Missing main Camera reference in {nameof(ObjectSelectionInputHandler)}");
            enabled = false;
        }

        /// <summary>
        /// Shoots a ray to see if a selectable object was on the position of the user's touch.
        /// </summary>
        /// <param name="touchPosition">The position of the user's touch</param>
        /// <returns><c>true</c> if a selectable object is found at the position, otherwise <c>false</c>.</returns>
        public bool HandleUserTouchedScreen(Vector2 touchPosition)
        {
            if (!enabled) return false;

            Ray ray = _camera.ScreenPointToRay(touchPosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                ISelectable selectable = hit.transform.GetComponent<ISelectable>();
                if (selectable != null)
                {
                    EventManager.InputEvent.Touch.OnObjectSelected.RaiseEvent(hit.collider.gameObject);
                    return true;
                }
            }

            return false;
        }
    }
}