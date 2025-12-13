using NLKARExperience.Core.Interfaces.Managers.AR;
using NLKARExperience.Core.Models;

using System.Collections.Generic;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Managers
{
    public class ARSpawnableObjectsManager : MonoBehaviour, ISpawnableARObjectsManager
    {
        [SerializeField] private ARSpawnableObject[] spawnableObjects;
        private Dictionary<int, ARSpawnableObject> _availibleObjects = new Dictionary<int, ARSpawnableObject>();

        void Awake()
        {
            for (int i = 0; i < spawnableObjects.Length; i++) { 
                if (spawnableObjects[i].ObjectPrefab == null)
                {
                    Logger.Log(LogSeverityLevel.Warning, $"ARSpawnableObject {spawnableObjects[i].Name} has no assigned gamePrefab in {nameof(ARSpawnableObjectsManager)}");
                    continue;
                }

                _availibleObjects.TryAdd(i, spawnableObjects[i]);
            }
        }

        void Start()
        {
            if (spawnableObjects.Length != 0) return;

            Logger.Log(LogSeverityLevel.Warning, $"No availible ARSpawnableObjects listed in {nameof(ARSpawnableObjectsManager)}");
            enabled = false;
        }

        public ARSpawnableObject GetARSpawnableObject(int id)
        {
            if (!enabled) return null;

            if (_availibleObjects.TryGetValue(id, out var spawnableObject))
            {
                return spawnableObject;
            }
            else
            {
                Logger.Log(LogSeverityLevel.Warning, $"{nameof(ARSpawnableObjectsManager)} is missing a reference to the following object with id {id.ToString()}");
                return null;
            }
        }
    }
}