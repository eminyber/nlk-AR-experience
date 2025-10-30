using NLKARExperience.Core.Interfaces.Services;

using UnityEngine;

namespace NLKARExperience.Services
{
    /// <summary>
    /// Handles instantiation of GameObjects in the scene.
    /// </summary>
    public class ObjectSpawner : IObjectSpawner
    {
        /// <summary>
        /// Spawns a prefab at a optinally given position, rotation, and parent transform.
        /// </summary>
        public GameObject SpawnAt(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            if (prefab == null) return null;

            GameObject newObject = Object.Instantiate(prefab, position, rotation, parent);
            return newObject;
        }
    }
}