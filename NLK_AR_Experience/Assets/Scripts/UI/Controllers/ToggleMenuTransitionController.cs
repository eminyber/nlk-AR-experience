using NLKARExperience.Core.Interfaces.Controllers.UI;
using NLKARExperience.Core.Interfaces.Managers.UI;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.UI.Controllers
{
    public class ToggleMenuTransitionController : MonoBehaviour, IMenuTransitionController
    {
        [SerializeField] private MonoBehaviour menuManager;

        private IUIMenuManager _menuManager;

        void Start()
        {
            if (menuManager == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing IUIMenuManager reference in {nameof(ToggleMenuTransitionController)}");
                enabled = false;
                return;
            }

            if (menuManager is not IUIMenuManager)
            {
                Logger.Log(LogSeverityLevel.Error, $"The cached IUIMenuManager reference is of wrong type in {nameof(ToggleMenuTransitionController)}");
                enabled = false;
                return;
            }

            _menuManager = (IUIMenuManager)menuManager;
        }

        public void Transition(MenuId fromMenu, MenuId toMenu)
        {
            if (!enabled) return;

            var (fromMenuPanel, toMenuPanel) = getMenus(fromMenu, toMenu);
            if (fromMenuPanel == null || toMenuPanel == null)
            {
                return;
            }

            fromMenuPanel.SetActive(false);
            toMenuPanel.SetActive(true);
        }

        private (GameObject fromMenuPanel, GameObject toMenuPanel) getMenus(MenuId fromMenu, MenuId toMenu) => (_menuManager.GetMenu(fromMenu), _menuManager.GetMenu(toMenu));
    }
}
