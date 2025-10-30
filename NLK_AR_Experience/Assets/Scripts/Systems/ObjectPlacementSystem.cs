using NLKARExperience.Core.EventSystem;
using NLKARExperience.Core.Interfaces.Services;
using NLKARExperience.Services;
using NLKARExperience.Util.Enums;

using UnityEngine;

using Logger = NLKARExperience.Util.Logger;

namespace NLKARExperience.Systems
{
    public class PrefabPlacementSystem : MonoBehaviour
    {
        [SerializeField] GameObject _gameObjectPrefab;

        private IObjectSpawner _objectSpawner = new ObjectSpawner();

        void OnEnable()
        {
            EventManager.InputEvent.Touch.OnPlacementPoseSelected.AddListener(handlePlacementPose);    
        }

        void Start()
        {
            if (_gameObjectPrefab == null)
            {
                Logger.LogMessage(LogSeverityLevel.Error, $"Missing GameObject reference in {nameof(PrefabPlacementSystem)}");
                enabled = false;
                return;
            }

            if (_objectSpawner == null)
            {
                Logger.LogMessage(LogSeverityLevel.Error, $"Missing IObjectSpawner reference in {nameof(PrefabPlacementSystem)}");
                enabled = false;
                return;
            } 
        }

        void OnDisable()
        {
            EventManager.InputEvent.Touch.OnPlacementPoseSelected.RemoveListener(handlePlacementPose);
        }

        private void handlePlacementPose(Pose pose)
        {
            if (pose == null)
            {
                Logger.LogMessage(LogSeverityLevel.Error,$"{nameof(PrefabPlacementSystem)}.{nameof(handlePlacementPose)}: Received null Pose argument.");
                return;
            }

            GameObject newGameObject = _objectSpawner.SpawnAt(_gameObjectPrefab, pose.position, pose.rotation);
            if (newGameObject == null) 
            {
                Logger.LogMessage(LogSeverityLevel.Warning, $"{nameof(PrefabPlacementSystem)}.{nameof(handlePlacementPose)}: Could not instatiate a new GameObject");
            }
        }
    }
}