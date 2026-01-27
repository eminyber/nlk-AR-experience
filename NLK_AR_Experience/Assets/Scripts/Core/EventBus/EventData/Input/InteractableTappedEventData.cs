using UnityEngine;

namespace NLKARExperience.Core.EventBus.EventData.Input
{
    public readonly struct InteractableTappedEventData
    {
        public Transform SelectedTransform { get; }

        public InteractableTappedEventData(Transform transform)
        {
            SelectedTransform = transform;
        }
    }
}