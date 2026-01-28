using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.Interfaces.Handlers.UI;
using NLKARExperience.Core.EventBus.EventData.System;

using UnityEngine;

public class ResetSceneBtnHandler : MonoBehaviour, IButtonClickHandler
{
    public void OnButtonClick()
    {
        EventBus.Publish<ResetCurrentSceneRequestedEventData>(new ResetCurrentSceneRequestedEventData());
    }
}
