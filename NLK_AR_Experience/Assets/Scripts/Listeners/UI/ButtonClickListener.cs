using NLKARExperience.Util.Enums;
using NLKARExperience.Core.Interfaces.Handlers;

using UnityEngine;

using Logger = NLKARExperience.Util.Logger;

namespace NLKARExperience.Listeners
{
    public class ButtonClickListener : MonoBehaviour
    {
        private IButtonClickHandler _buttonClickHandler;

        void OnEnable()
        {
            _buttonClickHandler = GetComponent<IButtonClickHandler>();           
        }

        void Start()
        {
            if (_buttonClickHandler != null) return;

            Logger.LogMessage(LogSeverityLevel.Error, $"Missing IButtonClickHandler in {nameof(ButtonClickListener)}");
            enabled = false;
        }

        public void OnButtonClick()
        {
            if (!enabled) return;

            _buttonClickHandler.HandleButtonClick();
        }
    }
}