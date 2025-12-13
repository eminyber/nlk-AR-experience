using System;

namespace NLKARExperience.Core.Interfaces.Animations
{
    public interface IAnimation
    {
        public bool IsPlaying{ get;}

        public void PlayAnimation(Action OnAnimationComplete = null);
    }
}