using NLKARExperience.Core.Interfaces.Registries;
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
        public MenuId ID;
        public GameObject Panel;
    }

    public class SceneMenusRegistry : MonoBehaviour, IObjectRegistry<MenuId, GameObject>
    {
        [SerializeField] private Menu[] menus;
        private Dictionary<MenuId, GameObject> _availableMenus = new Dictionary<MenuId, GameObject>();

        void Awake()
        {
            foreach (var menu in menus) {
                if (menu.Panel == null)
                {
                    Logger.Log(LogSeverityLevel.Warning, $"MenuType {menu.ID} has no assigned panel in {nameof(SceneMenusRegistry)}");
                    continue;
                }

                if (!_availableMenus.TryAdd(menu.ID, menu.Panel))
                {
                    Logger.Log(LogSeverityLevel.Warning, $"Duplicate MenuID {menu.ID} in {nameof(SceneMenusRegistry)}");
                }
            }
        }

        void Start()
        {
            if (menus.Length != 0) return;

            Logger.Log(LogSeverityLevel.Warning, $"No availible menus listed in {nameof(SceneMenusRegistry)}");
            enabled = false;
        }

        public bool TryGetObject(MenuId key, out GameObject registeredObject)
        {
            if (!enabled)
            {
                registeredObject = null;
                return false;
            }

            return _availableMenus.TryGetValue(key, out registeredObject);
        }
    }
}