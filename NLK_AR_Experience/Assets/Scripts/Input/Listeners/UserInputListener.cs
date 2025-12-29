using NLKARExperience.Core.Interfaces.Handlers.Input;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Utils;

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
            bool validationSucceeded = ValidateScriptDependencies();
            if (!validationSucceeded)
            {
                enabled = false;
                return;
            }

            EnhancedTouch.EnhancedTouchSupport.Enable();
            EnhancedTouch.Touch.onFingerDown += HandleOnFingerDown;
        }

        void OnDisable()
        {
            if (!EnhancedTouch.EnhancedTouchSupport.enabled) return;

            EnhancedTouch.EnhancedTouchSupport.Disable();
            EnhancedTouch.Touch.onFingerDown -= HandleOnFingerDown;
        }

        private void HandleOnFingerDown(EnhancedTouch.Finger finger)
        {
            if (finger.index != 0) return;

            _userInputHandler.ProcessInput(finger.screenPosition);
        }

        private bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<IUserInputHandler>(UserInputHandlerReference, out _userInputHandler))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(UserInputHandlerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IUserInputHandler' in {nameof(UserScreenInputListener)}");
                return false;
            }

            return true;
        }
    }
}