using System;

namespace NLKARExperience.Core.Interfaces.Animations
{
    public interface IUIAnimation
    {
        public bool IsPlaying { get; }
        public void PlayEnterAnimation(Action OnAnimationComplete = null);
        public void PlayExitAnimation(Action OnAnimationComplete = null);
    }
}