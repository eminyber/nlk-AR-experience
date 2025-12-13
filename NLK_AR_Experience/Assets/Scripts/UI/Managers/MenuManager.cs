using NLKARExperience.Core.Interfaces.Managers.UI;
using NLKARExperience.Core.Models;

using System;
using System.Collections.Generic;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.UI.Managers
{
    [Serializable]
    public class Menu
    {
        public MenuId Type;
        public GameObject Panel;
    }

    public class MenuManager : MonoBehaviour, IUIMenuManager
    {
        [SerializeField] private Menu[] menus;

        private Dictionary<MenuId, GameObject> _availibleMenus = new Dictionary<MenuId, GameObject>();

        void Awake()
        {
            foreach (var menu in menus) {
                if (menu.Panel == null)
                {
                    Logger.Log(LogSeverityLevel.Warning, $"MenuType {menu.Type} has no assigned panel in {nameof(MenuManager)}");
                    continue;
                }

                _availibleMenus.TryAdd(menu.Type, menu.Panel);
            }
        }

        void Start()
        {
            if (menus.Length != 0) return;

            Logger.Log(LogSeverityLevel.Warning, $"No availible menus listed in {nameof(MenuManager)}");
            enabled = false;
        }

        public GameObject GetMenu(MenuId menuType)
        {
            if (!enabled) return null;

            if (_availibleMenus.TryGetValue(menuType, out var menuPanel))
            {
                return menuPanel;
            }
            else
            {
                Logger.Log(LogSeverityLevel.Error, $"{nameof(MenuManager)} is missing a reference to the following MenuType {menuType.ToString()}");
                return null;
            }
        }
    }
}