using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.EventBus.EventData.UI;
using NLKARExperience.Core.Interfaces.Handlers.UI;
using NLKARExperience.Core.Models;

using UnityEngine;

namespace NLKARExperience.UI.Handlers.ButtonClicks
{
    public class MenuTransitionBtnHandler : MonoBehaviour, IButtonClickHandler
    {
        [SerializeField] MenuId transitionFromMenu;
        [SerializeField] MenuId transitionToMenu;

        public void OnButtonClick()
        {
            EventBus.Publish<MenuTransitionRequestedEventDatas>(new MenuTransitionRequestedEventDatas(transitionFromMenu, transitionToMenu));
        }
    }
}