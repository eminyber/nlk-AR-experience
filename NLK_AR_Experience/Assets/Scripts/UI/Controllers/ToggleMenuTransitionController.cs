using NLKARExperience.Core.Interfaces.Controllers.UI;
using NLKARExperience.Core.Interfaces.Registries;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.UI.Controllers
{
    public class ToggleMenuTransitionController : MonoBehaviour, IMenuTransitionController
    {
        [SerializeField] private MonoBehaviour sceneMenusRegistryReference;
        private IObjectRegistry<MenuId, GameObject> _sceneMenusRegistry;

        void Start()
        {
            if (sceneMenusRegistryReference == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing IObjectRegistry<MenuId, GameObject> reference in {nameof(ToggleMenuTransitionController)}");
                enabled = false;
                return;
            }

            if (sceneMenusRegistryReference is not IObjectRegistry<MenuId, GameObject>)
            {
                Logger.Log(LogSeverityLevel.Error, $"The cached IObjectRegistry<MenuId, GameObject> reference is of wrong type in {nameof(ToggleMenuTransitionController)}");
                enabled = false;
                return;
            }

            _sceneMenusRegistry = (IObjectRegistry<MenuId, GameObject>) sceneMenusRegistryReference;
        }

        public void Transition(MenuId fromMenuID, MenuId toMenuID)
        {
            if (!enabled) return;

            var (fromMenu, toMenu) = getMenus(fromMenuID, toMenuID);
            if (fromMenu == null || toMenu == null)
            {
                return;
            }

            fromMenu.SetActive(false);
            toMenu.SetActive(true);
        }


        private (GameObject fromMenuPanel, GameObject toMenuPanel) getMenus(MenuId fromMenuID, MenuId toMenuID)
        {
            GameObject fromMenu;
            if(!_sceneMenusRegistry.TryGetObject(fromMenuID, out fromMenu))
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
    }
}
