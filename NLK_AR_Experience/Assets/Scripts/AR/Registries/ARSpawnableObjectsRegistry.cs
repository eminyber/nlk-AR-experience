using NLKARExperience.Core.Interfaces.Registries;
using NLKARExperience.Core.Models;

using System.Collections.Generic;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Registries
{
    public class ARSpawnableObjectsRegistry : MonoBehaviour, IObjectRegistry<int, ARSpawnableObject>
    {
        [SerializeField] private ARSpawnableObject[] spawnableObjects;
        private Dictionary<int, ARSpawnableObject> _availibleObjects = new Dictionary<int, ARSpawnableObject>();

        void Awake()
        {
            for (int i = 0; i < spawnableObjects.Length; i++) { 
                if (spawnableObjects[i].ObjectPrefab == null)
                {
                    Logger.Log(LogSeverityLevel.Warning, $"ARSpawnableObject {spawnableObjects[i].Name} has no assigned gamePrefab in {nameof(ARSpawnableObjectsRegistry)}");
                    continue;
                }

                _availibleObjects.TryAdd(i, spawnableObjects[i]);
            }
        }

        void Start()
        {
            if (spawnableObjects.Length != 0) return;

            Logger.Log(LogSeverityLevel.Warning, $"No availible ARSpawnableObjects listed in {nameof(ARSpawnableObjectsRegistry)}");
            enabled = false;
        }

        public bool TryGetObject(int key, out ARSpawnableObject arSpawnable)
        {
            if (!enabled)
            {
                arSpawnable = null;
                return false;
            }

            return _availibleObjects.TryGetValue(key, out arSpawnable);
        }
    }
}