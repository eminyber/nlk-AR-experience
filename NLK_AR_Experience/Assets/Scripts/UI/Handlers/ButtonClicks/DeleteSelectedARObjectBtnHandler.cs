using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.EventBus.EventData.AR;
using NLKARExperience.Core.Interfaces.Handlers.UI;

using UnityEngine;

public class DeleteSelectedARObjectBtnHandler : MonoBehaviour, IButtonClickHandler
{
    public void OnButtonClick()
    {
        EventBus.Publish<DeleteSelectedARObjectEventData>(new DeleteSelectedARObjectEventData());
    }
}
