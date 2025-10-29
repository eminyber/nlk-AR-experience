using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Util.Enums;

using UnityEngine;

using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using Logger = NLKARExperience.Util.Logger;

namespace NLKARExperience.Listeners
{
    /// <summary>
    /// Listens for raw screen touch input via Unity's Enhanced Touch system.
    /// </summary>
    /// <remarks>
    /// This class acts as a bridge, translating low-level <see cref="EnhancedTouch.Touch"/> 
    /// events into a higher-level call on an <see cref="IUserInputHandler"/>.
    /// <para>
    /// It requires an <see cref="IUserInputHandler"/> component to be on the same GameObject.
    /// </para>
    /// </remarks>
    public class ScreenTouchEventListener : MonoBehaviour
    {
        /// <summary>
        /// Cached reference to the user input handler found on this GameObject.
        /// </summary>
        private IUserInputHandler _userInputHandler;

        /// <summary>
        /// Caches the input handler and subscribes to the Enhanced Touch event.
        /// </summary>
        void OnEnable()
        {
            _userInputHandler = GetComponent<IUserInputHandler>();

            EnhancedTouch.EnhancedTouchSupport.Enable();
            EnhancedTouch.Touch.onFingerDown += handleOnFingerDown;
        }

        /// <summary>
        /// Validates that the <see cref="IUserInputHandler"/> dependency was found.
        /// </summary>
        /// <remarks>
        /// If the handler is missing, this component will log an error and disable itself 
        /// to prevent null reference exceptions.
        /// </remarks>
        void Start()
        {
            if (_userInputHandler != null) return;

            Logger.LogMessage(LogSeverityLevel.Error, $"Missing UserInputHandler reference on {nameof(ScreenTouchEventListener)}.");
            enabled = false;
        }

        /// <summary>
        /// Disables Enhanced Touch support and unsubscribes from the touch event.
        /// </summary>
        void OnDisable()
        {
            if (EnhancedTouch.EnhancedTouchSupport.enabled)
            {
                EnhancedTouch.EnhancedTouchSupport.Disable();
                EnhancedTouch.Touch.onFingerDown -= handleOnFingerDown;
            }
        }

        /// <summary>
        /// Callback handler for the <see cref="EnhancedTouch.Touch.onFingerDown"/> event.
        /// </summary>
        /// <remarks>
        /// This method filters for the first finger (index 0) only and passes 
        /// the screen position to the <see cref="_userInputHandler"/>.
        /// </remarks>
        /// <param name="finger">The <see cref="EnhancedTouch.Finger"/> Data for the touch that just occurred.</param>
        private void handleOnFingerDown(EnhancedTouch.Finger finger)
        {
            if (finger.index != 0) return;

            _userInputHandler.HandleUserTouchedScreen(finger.screenPosition);
        }
    }
}