using NLKARExperience.Core.Interfaces.Animations;
using NLKARExperience.Core.Utils;
using NLKARExperience.Core.Models;

using System;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.UI.Animations
{
    public class SlideInOutUIAnimation : MonoBehaviour, IUIAnimation
    {
        [Header("Animation Settings")]
        [SerializeField, Min(0f)] float animationTime = 0f;
        [SerializeField, Min(0f)] float animationDelay = 0f;
        [SerializeField] LeanTweenType easeType = LeanTweenType.notUsed;

        [Header("Start Position")]
        [SerializeField] Direction slideInAndOutDirection = Direction.Right;
        [SerializeField] bool startOutsideScreen = false;

        bool IUIAnimation.IsPlaying { get => _isPlaying; }
        private bool _isPlaying = false;

        private RectTransform _rectTransform;
        private Vector2 _currentScreenPosition;
        private Vector2 _screenOffset;

        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            if (_rectTransform != null)
            {
                _currentScreenPosition = _rectTransform.anchoredPosition;
                setScreenOffset();
                if (startOutsideScreen)
                    _rectTransform.anchoredPosition = _currentScreenPosition + _screenOffset;
                    
            }
        }

        void Start()
        {
            if (_rectTransform == null)
            {
                Logger.Log(LogSeverityLevel.Warning, $"Missing RectTransform component on the object where the ");
                enabled = false;
                return;
            }
        }

        public void PlayEnterAnimation(Action OnAnimationComplete = null)
        {
            _isPlaying = true;

            LeanTween.move(_rectTransform, _currentScreenPosition, animationTime)
               .setDelay(animationDelay)
               .setEase(easeType)
               .setOnComplete(() =>
               {
                   _isPlaying = false;
                   OnAnimationComplete?.Invoke();
               });
        }

        public void PlayExitAnimation(Action OnAnimationComplete = null)
        {
            _isPlaying = true;

            Vector2 targetPosition = _rectTransform.anchoredPosition + _screenOffset;
            LeanTween.move(_rectTransform, targetPosition, animationTime)
                .setDelay(animationDelay)
                .setEase(easeType)
                .setOnComplete(() =>
                {
                    _isPlaying = false;
                    OnAnimationComplete?.Invoke();
                });
        }

        private void setScreenOffset()
        {
            _screenOffset = ScreenDimensionUtility.GetOffset(slideInAndOutDirection);
        }
    }
}