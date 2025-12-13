using NLKARExperience.Core.Interfaces.Handlers.Input;
using NLKARExperience.Core.Models;

using UnityEngine;

using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.Input.Listeners
{
    public class UserScreenInputListener : MonoBehaviour
    {
        [SerializeField] MonoBehaviour UserInputHandlerReference;

        private IUserInputHandler _userInputHandler;

        void Start()
        {
            if (UserInputHandlerReference == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing IUserInputHandler reference in {nameof(UserScreenInputListener)}");
                enabled = false;
                return;
            }

            if (UserInputHandlerReference is not IUserInputHandler)
            {
                Logger.Log(LogSeverityLevel.Error, $"The cached MonoBehvaiour reference is not of type IUserInputHandler in {nameof(UserScreenInputListener)}");
                enabled = false;
                return;
            }

            EnhancedTouch.EnhancedTouchSupport.Enable();
            EnhancedTouch.Touch.onFingerDown += handleOnFingerDown;

            _userInputHandler = (IUserInputHandler)UserInputHandlerReference;
        }

        void OnDisable()
        {
            if (!EnhancedTouch.EnhancedTouchSupport.enabled) return;

            EnhancedTouch.EnhancedTouchSupport.Disable();
            EnhancedTouch.Touch.onFingerDown -= handleOnFingerDown;
        }

        private void handleOnFingerDown(EnhancedTouch.Finger finger)
        {
            if (finger.index != 0) return;

            _userInputHandler.ProcessInput(finger.screenPosition);
        }
    }
}