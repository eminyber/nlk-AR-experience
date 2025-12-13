using NLKARExperience.Core.Interfaces.Animations;

using System;

using UnityEngine;

namespace NLKARExperience.AR.Animations
{
    public class ScaleAnimation : MonoBehaviour, IAnimation
    {
        [SerializeField] Vector3 scaleTo = new Vector3();

        [SerializeField, Min(0f)] float scaleTime = 0f;
        [SerializeField, Min(0f)] float animationDelay = 0f;

        [SerializeField] LeanTweenType easeType = LeanTweenType.notUsed;

        bool IAnimation.IsPlaying { get => _isPlaying; }
        private bool _isPlaying = false;

        public void PlayAnimation(Action OnAnimationComplete = null)
        {
            _isPlaying = true;

            LeanTween.scale(gameObject, scaleTo, scaleTime)
                .setDelay(animationDelay)
                .setEase(easeType)
                .setOnComplete(() =>
                {
                    _isPlaying = false;
                    OnAnimationComplete?.Invoke();
                });
        }
    }
}
