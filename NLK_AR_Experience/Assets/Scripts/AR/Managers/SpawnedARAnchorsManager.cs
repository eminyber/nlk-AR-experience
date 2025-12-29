using NLKARExperience.Core.Interfaces.Managers;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace NLKARExperience.AR.Managers
{
    public class SpawnedARAnchorsManager : MonoBehaviour, ISpawnedObjectsManager<ARAnchor>
    {
        private Dictionary<int, ARAnchor> _spawnedARAnchors = new Dictionary<int, ARAnchor>();

        public bool AddSpawnedObject(ARAnchor spawnedObject)
        {
            if (spawnedObject == null) return false;

            return _spawnedARAnchors.TryAdd(spawnedObject.GetInstanceID(), spawnedObject);
        }

        public ARAnchor RemoveSpawnedObject(int instanceID)
        {
            if (_spawnedARAnchors.Remove(instanceID, out var obj))
            {
                return obj;
            }
            return null;
        }

        public int GetSpawnedObjectsCount()
        {
            return _spawnedARAnchors.Count;
        }
    }
}