using NLKARExperience.Core.Interfaces.Animations;

using System;

using UnityEngine;

namespace NLKARExperience.AR.Animations
{
    public class FadeAnimation : MonoBehaviour, IAnimation
    {
        [SerializeField, Range(0f, 1f)] float fadeToValue = 0f;

        [SerializeField, Min(0f)] float fadeDuration = 0f;
        [SerializeField, Min(0f)] float animationDelay = 0f;

        [SerializeField] LeanTweenType easeType = LeanTweenType.notUsed;

        bool IAnimation.IsPlaying { get => _isPlaying; }

        private bool _isPlaying = false;

        public void PlayAnimation(Action OnAnimationComplete = null)
        {
            _isPlaying = true;

            gameObject.LeanAlpha(fadeToValue, fadeDuration)
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