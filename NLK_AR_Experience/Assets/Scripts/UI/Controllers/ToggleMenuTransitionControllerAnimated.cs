using NLKARExperience.Core.Interfaces.Controllers.UI;
using NLKARExperience.Core.Interfaces.Animations;
using NLKARExperience.Core.Interfaces.Registries;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.UI.Controllers
{
    public class ToggleMenuTransitionControllerAnimated : MonoBehaviour, IMenuTransitionController
    {
        [SerializeField] private MonoBehaviour sceneMenusRegistryReference;
        [SerializeField] private MonoBehaviour UIAnimationControllerReference;

        private IObjectRegistry<MenuId, GameObject> _sceneMenusRegistry;
        private IUIAnimationController _animationController;

        void Start()
        {
           validateDependencies();
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

        private (GameObject fromMenuPanel, GameObject toMenuPanel) getMenus(MenuId fromMenuID, MenuId toMenuID)
        {
            GameObject fromMenu;
            if (!_sceneMenusRegistry.TryGetObject(fromMenuID, out fromMenu))
            {
                Logger.Log(LogSeverityLevel.Error, $"Could not retrive menu with ID: {fromMenuID} from the registry in {nameof(ToggleMenuTransitionController)}");
                return (null, null);
            }

            GameObject toMenu;
            if (!_sceneMenusRegistry.TryGetObject(toMenuID, out toMenu))
            {
                Logger.Log(LogSeverityLevel.Error, $"Could not retrive menu with ID: {toMenu} from the registry in {nameof(ToggleMenuTransitionController)}");
                return (null, null);
            }

            return (fromMenu, toMenu);
        }

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

        private void validateDependencies()
        {
            if (sceneMenusRegistryReference == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing IObjectRegistry<MenuId, GameObject> reference in {nameof(ToggleMenuTransitionControllerAnimated)}");
                enabled = false;
                return;
            }

            if (UIAnimationControllerReference == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing IUIAnimationController reference in {nameof(ToggleMenuTransitionControllerAnimated)}");
                enabled = false;
                return;
            }

            if (sceneMenusRegistryReference is not IObjectRegistry<MenuId, GameObject>)
            {
                Logger.Log(LogSeverityLevel.Error, $"The cached IObjectRegistry<MenuId, GameObject> reference is of wrong type in {nameof(ToggleMenuTransitionControllerAnimated)}");
                enabled = false;
                return;
            }

            if (UIAnimationControllerReference is not IUIAnimationController)
            {
                Logger.Log(LogSeverityLevel.Error, $"The cached IUIAnimationController reference is of wrong type in {nameof(ToggleMenuTransitionControllerAnimated)}");
                enabled = false;
                return;
            }

            _sceneMenusRegistry = (IObjectRegistry<MenuId, GameObject>) sceneMenusRegistryReference;
            _animationController = (IUIAnimationController) UIAnimationControllerReference;

        }
    }
}