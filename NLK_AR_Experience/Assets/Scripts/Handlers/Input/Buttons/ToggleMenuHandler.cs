using NLKARExperience.Core.EventSystem;
using NLKARExperience.Core.Interfaces.Handlers;

using UnityEngine;

namespace NLKARExperience.Handlers
{
    public class ToggleMenuHandler : MonoBehaviour, IButtonClickHandler
    {
        public void HandleButtonClick()
        {
            EventManager.InputEvent.ButtonClick.OnMenuTogggled.RaiseEvent();
        }
    }
}
