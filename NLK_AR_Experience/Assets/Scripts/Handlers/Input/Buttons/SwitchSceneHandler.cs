using NLKARExperience.Core.EventSystem;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Util.Enums;

using UnityEngine;

namespace NLKARExperience.Handlers
{
    public class SwitchSceneHandler : MonoBehaviour, IButtonClickHandler
    {
        [SerializeField] AppScene _sceneToSwitchTo;

        public void HandleButtonClick()
        {
            EventManager.AppEvent.Scene.OnSceneSwitch.RaiseEvent(_sceneToSwitchTo);    
        }
    }
}