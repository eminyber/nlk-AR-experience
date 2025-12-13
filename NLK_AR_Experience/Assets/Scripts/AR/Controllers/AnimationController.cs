using NLKARExperience.Core.Interfaces.Animations;

using System;

namespace NLKARExperience.AR.Controllers
{
    public class AnimationController
    {
        public void PlayAnimation(IAnimation animation, Action onAnimationComplete = null)
        {
            animation.PlayAnimation(onAnimationComplete);
        }
    }
}