using NLKARExperience.Core.Interfaces.Managers;

using System.Collections.Generic;

using UnityEngine;

namespace NLKARExperience.AR.Managers
{
    public class ARSpawnedObjects : MonoBehaviour, ISpawnedObjectsManager<GameObject>
    {
        private Dictionary<int, GameObject> spawnedObjects = new Dictionary<int, GameObject>();

        public bool AddSpawnedObject(GameObject spawnedObject)
        {
            if (spawnedObject == null) return false;

            return spawnedObjects.TryAdd(spawnedObject.GetInstanceID(), spawnedObject);
        }

        public GameObject RemoveSpawnedObject(int instanceID)
        {
            if (spawnedObjects.Remove(instanceID, out var obj))
            {
                return obj;
            }
            return null;
        }

        public int GetSpawnedObjectsCount() { return spawnedObjects.Count; }
    }
}