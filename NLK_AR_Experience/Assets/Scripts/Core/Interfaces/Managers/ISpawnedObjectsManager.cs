using UnityEngine;

namespace NLKARExperience.Core.Interfaces.Managers
{
    public interface ISpawnedObjectsManager<T> where T : Object
    {
        bool AddSpawnedObject(T spawnedObject);
        T RemoveSpawnedObject(int instanceID);
        int GetSpawnedObjectsCount();
    }
}