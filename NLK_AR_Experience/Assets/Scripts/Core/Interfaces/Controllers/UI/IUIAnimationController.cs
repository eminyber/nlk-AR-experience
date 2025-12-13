using NLKARExperience.Core.Interfaces.Animations;

using UnityEngine;

namespace NLKARExperience.Core.Interfaces.Controllers.UI
{
    public interface IUIAnimationController
    {
        public void PlayShowAnimation(IUIAnimation animation);
        public void PlayHideAnimation(IUIAnimation animation, GameObject objectToHide);
    }
}