using NLKARExperience.Core.Interfaces.Animations;

using System;

using UnityEngine;

namespace NLKARExperience.AR.Animations
{
    public class MoveAnimationLocalSpace : MonoBehaviour, IAnimation
    {
        [Header("Animation Settings")]
        [SerializeField] Vector3 moveTo = new Vector3();
        [SerializeField, Min(0f)] float moveTime = 0f;
        [SerializeField, Min(0f)] float animationDelay = 0f;
        [SerializeField] LeanTweenType easeType = LeanTweenType.notUsed;

        [Header("Start Position Settings")]
        [SerializeField] bool customStartPosition = false;
        [SerializeField] Vector3 startPosition = new Vector3();

        bool IAnimation.IsPlaying { get => _isPlaying; }
        private bool _isPlaying = false;

        void Awake()
        {
            if (customStartPosition)
                transform.localPosition = startPosition;
        }

        public void PlayAnimation(Action OnAnimationComplete = null)
        {
            _isPlaying = true;

            transform.LeanMoveLocal(moveTo, moveTime)
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