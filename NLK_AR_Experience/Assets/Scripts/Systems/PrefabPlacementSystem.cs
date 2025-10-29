using NLKARExperience.Core.EventSystem;
using NLKARExperience.Util.Enums;

using UnityEngine;

using Logger = NLKARExperience.Util.Logger;

namespace NLKARExperience.Systems
{
    public class PrefabPlacementSystem : MonoBehaviour
    {
        void OnEnable()
        {
            EventManager.InputEvent.Touch.OnPlacementPoseSelected.AddListener(handlePlacementPose);    
        }

        void OnDisable()
        {
            EventManager.InputEvent.Touch.OnPlacementPoseSelected.RemoveListener(handlePlacementPose);
        }

        private void handlePlacementPose(Pose pose)
        {
            Logger.LogMessage(LogSeverityLevel.Info, $"in {nameof(PrefabPlacementSystem)}");
        }
    }
}