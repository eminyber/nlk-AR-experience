using UnityEngine;

namespace NLKARExperience.Core.Interfaces.Services
{
    public interface IObjectSpawner
    {
        public GameObject SpawnAt(GameObject prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null);
    }
}