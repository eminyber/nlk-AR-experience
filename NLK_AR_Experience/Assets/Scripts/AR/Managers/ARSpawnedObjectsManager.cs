using NLKARExperience.Core.Interfaces.Managers;

using System.Collections.Generic;

using UnityEngine;

namespace NLKARExperience.AR.Managers
{
    public class ARSpawnedObjectsManager : MonoBehaviour, ISpawnedObjectsManager<GameObject>
    {
        private Dictionary<int, GameObject> _spawnedObjects = new Dictionary<int, GameObject>();

        public bool AddSpawnedObject(GameObject spawnedObject)
        {
            if (spawnedObject == null) return false;

            return _spawnedObjects.TryAdd(spawnedObject.GetInstanceID(), spawnedObject);
        }

        public GameObject RemoveSpawnedObject(int instanceID)
        {
            if (_spawnedObjects.Remove(instanceID, out var obj))
            {
                return obj;
            }
            return null;
        }

        public int GetSpawnedObjectsCount() { return _spawnedObjects.Count; }
    }
}