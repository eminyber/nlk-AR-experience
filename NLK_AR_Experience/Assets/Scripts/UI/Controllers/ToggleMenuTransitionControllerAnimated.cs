using NLKARExperience.Core.Interfaces.Controllers.UI;
using NLKARExperience.Core.Interfaces.Animations;
using NLKARExperience.Core.Interfaces.Managers.UI;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.UI.Controllers
{
    public class ToggleMenuTransitionControllerAnimated : MonoBehaviour, IMenuTransitionController
    {
        [SerializeField] private MonoBehaviour menuManagerReference;
        [SerializeField] private MonoBehaviour UIAnimationControllerReference;

        private IUIMenuManager _menuManager;
        private IUIAnimationController _animationController;

        void Start()
        {
            if (menuManagerReference == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing IUIMenuManager reference in {nameof(ToggleMenuTransitionControllerAnimated)}");
                enabled = false;
                return;
            }

            if (UIAnimationControllerReference == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing IUIAnimationController reference in {nameof(ToggleMenuTransitionControllerAnimated)}");
                enabled = false;
                return;
            }

            if (menuManagerReference is not IUIMenuManager)
            {
                Logger.Log(LogSeverityLevel.Error, $"The cached IUIMenuManager reference is of wrong type in {nameof(ToggleMenuTransitionControllerAnimated)}");
                enabled = false;
                return;
            }

            if (UIAnimationControllerReference is not IUIAnimationController)
            {
                Logger.Log(LogSeverityLevel.Error, $"The cached IUIAnimationController reference is of wrong type in {nameof(ToggleMenuTransitionControllerAnimated)}");
                enabled = false;
                return;
            }

            _menuManager = (IUIMenuManager)menuManagerReference;
            _animationController = (IUIAnimationController)UIAnimationControllerReference;
        }

        public void Transition(MenuId fromMenu, MenuId toMenu)
        {
            if (!enabled) return;

            var (fromMenuPanel, toMenuPanel) = getMenus(fromMenu, toMenu);
            if (fromMenuPanel == null || toMenuPanel == null)
            {
                return;
            }

            handleMenuTransition(fromMenuPanel, false);
            handleMenuTransition(toMenuPanel, true);
        }

        private (GameObject fromMenuPanel, GameObject toMenuPanel) getMenus(MenuId fromMenu, MenuId toMenu) => (_menuManager.GetMenu(fromMenu), _menuManager.GetMenu(toMenu));

        private void handleMenuTransition(GameObject panel, bool show)
        {
            var UIAnimation = panel.GetComponent<IUIAnimation>();
            if (UIAnimation == null)
            {
                logMissingUIAnimation(panel.name);
                panel.SetActive(show);
                return;
            }

            if (show)
            {
                panel.SetActive(true);
                _animationController.PlayShowAnimation(UIAnimation);
            }
            else
            {
                _animationController.PlayHideAnimation(UIAnimation, panel);
            }
        }

        private void logMissingUIAnimation(string objectName)
        {
            Logger.Log(LogSeverityLevel.Warning, $"{objectName}: has missing IUIAnimationController reference");
        }
    }
}