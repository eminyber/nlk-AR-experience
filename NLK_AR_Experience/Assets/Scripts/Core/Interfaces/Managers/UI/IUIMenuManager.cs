using NLKARExperience.Core.Models;

using UnityEngine;

namespace NLKARExperience.Core.Interfaces.Managers.UI
{
    public interface IUIMenuManager 
    {
        public GameObject GetMenu(MenuId menuId);
    }
}