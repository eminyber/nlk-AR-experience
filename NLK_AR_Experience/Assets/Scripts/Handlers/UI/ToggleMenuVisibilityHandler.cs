using NLKARExperience.Core.EventSystem;
using NLKARExperience.Util.Enums;

using UnityEngine;

using Logger = NLKARExperience.Util.Logger;

namespace NLKARExperience.Handlers
{
    public class ToggleMenuVisibilityHandler : MonoBehaviour
    {
        [SerializeField] GameObject _objectToToggle;

        void OnEnable()
        {
            EventManager.InputEvent.ButtonClick.OnMenuTogggled.AddListener(handleMenuToggled);
        }

        void Start()
        {
            if (_objectToToggle != null) return;

            Logger.LogMessage(LogSeverityLevel.Error, $"Missing GameObject reference in {nameof(ToggleMenuVisibilityHandler)}");
            enabled = false;
        }

        private void OnDisable()
        {
            EventManager.InputEvent.ButtonClick.OnMenuTogggled.RemoveListener(handleMenuToggled);
        }

        private void handleMenuToggled()
        {
            if (_objectToToggle == null) return;

            _objectToToggle.SetActive(!_objectToToggle.activeSelf);
        }
    }
}