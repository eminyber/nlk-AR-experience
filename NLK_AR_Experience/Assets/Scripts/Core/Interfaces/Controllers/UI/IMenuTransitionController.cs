using NLKARExperience.Core.Models;

namespace NLKARExperience.Core.Interfaces.Controllers.UI
{
    public interface IMenuTransitionController
    {
        public void Transition(MenuId fromMenu, MenuId toMenu);
    }
}