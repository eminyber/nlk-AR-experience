using UnityEngine;

namespace NLKARExperience.Core.EventBus.EventData.Input
{
    public readonly struct InteractableObjectTappedEventData
    {
        public Transform SelectedTransform { get; }

        public InteractableObjectTappedEventData(Transform transform)
        {
            SelectedTransform = transform;
        }
    }
}