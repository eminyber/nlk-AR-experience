using NLKARExperience.Core.EventSystem.EventTypes;

using UnityEngine;

namespace NLKARExperience.Core.EventSystem.Events.InputEvents
{
    public class TouchEvents
    {
        public readonly Event<Pose> OnPlacementPoseSelected = new Event<Pose>();
        public readonly Event<GameObject> OnObjectSelected = new Event<GameObject>();
    }
}