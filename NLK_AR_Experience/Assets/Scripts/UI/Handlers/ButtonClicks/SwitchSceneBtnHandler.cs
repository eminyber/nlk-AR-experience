using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.Interfaces.Handlers.UI;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.EventBus.EventData.System;

using UnityEngine;

namespace NLKARExperience.UI.Handlers.ButtonClicks
{
    public class SwitchSceneBtnHandler : MonoBehaviour, IButtonClickHandler
    {
        [SerializeField] AppScene newScene = AppScene.MainMenu;

        public void OnButtonClick()
        {
            EventBus.Publish(new SwitchSceneRequestedEventData(newScene));
        }
    }
}