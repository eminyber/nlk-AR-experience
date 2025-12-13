using NLKARExperience.Core.Interfaces.Controllers.UI;
using NLKARExperience.Core.Interfaces.Animations;

using UnityEngine;

namespace NLKARExperience.UI.Controllers
{
    public class UIAnimationController : MonoBehaviour, IUIAnimationController
    {
        public void PlayHideAnimation(IUIAnimation animation, GameObject objectToHide)
        {
            if (animation == null) return;

            if (!animation.IsPlaying)
                animation.PlayExitAnimation(() => objectToHide.SetActive(false));
        }

        public void PlayShowAnimation(IUIAnimation animation)
        {
            if (animation == null) return;

            if (!animation.IsPlaying)
                animation.PlayEnterAnimation();
        }
    }
}