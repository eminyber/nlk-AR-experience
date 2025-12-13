using NLKARExperience.Core.Interfaces.Handlers.UI;
using NLKARExperience.Core.EventBus.EventData.AR;
using NLKARExperience.Core.EventBus;

using UnityEngine;

namespace NLKARExperience.UI.Handlers.ButtonClicks
{
    public class SwitchSelectedARObjectBtnHandler : MonoBehaviour, IButtonClickHandler
    {
        [SerializeField] int newARObjectIndex = 0;

        public void OnButtonClick()
        {
            EventBus.Publish(new ARObjectToSpawnChangeRequestedEventData(newARObjectIndex));
        }
    }
}