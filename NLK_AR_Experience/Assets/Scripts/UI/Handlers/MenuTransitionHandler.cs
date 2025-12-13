using NLKARExperience.Core.EventBus.EventData.UI;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Interfaces.Controllers.UI;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.UI.Handlers
{
    public class MenuTransitionHandler : MonoBehaviour, IEventHandler<MenuTransitionRequestedEventDatas>
    {
        [SerializeField] MonoBehaviour menuTransitionControllerReference;

        private IMenuTransitionController _menuTransitionController;

        void Start()
        {
            if (menuTransitionControllerReference == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing IMenuTransitionController reference in {nameof(MenuTransitionHandler)}");
                enabled = false;
                return;
            }

            if (menuTransitionControllerReference is not IMenuTransitionController)
            {
                Logger.Log(LogSeverityLevel.Error, $"The referenced IMenuTransitionController is of wrong type in {nameof(MenuTransitionHandler)}");
                enabled = false;
                return;
            }

            _menuTransitionController = (IMenuTransitionController) menuTransitionControllerReference;
        }

        public void HandleEvent(MenuTransitionRequestedEventDatas eventData)
        {
            if (!enabled) return;

            _menuTransitionController.Transition(eventData.TransitionFrom, eventData.TransitionTo);
        }
    }
}