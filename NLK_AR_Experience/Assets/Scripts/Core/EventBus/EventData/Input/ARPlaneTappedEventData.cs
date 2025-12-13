using UnityEngine;

namespace NLKARExperience.Core.EventBus.EventData.Input
{
    public readonly struct ARPlaneTappedEventData
    {
        public Pose Pose { get; }
        public ARPlaneTappedEventData(Pose pose)
        {
            Pose = pose;
        }
    }
}