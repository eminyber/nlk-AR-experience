using NLKARExperience.Core.Interfaces.Systems;
using NLKARExperience.Core.Interfaces.Managers.AR;
using NLKARExperience.Core.Interfaces.Managers;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;


public class SimpleARSpawnObjectSystem : MonoBehaviour, ISpawnSystem
{
    [SerializeField] private MonoBehaviour spawnableARObjectsManagerReference;
    [SerializeField] private MonoBehaviour spawnObjectSelectionManagerReference;
    [SerializeField] private MonoBehaviour spawnedObjectsManagerReference;

    private ISpawnableARObjectsManager _spawnableARObjectsManager;
    private ISpawnObjectSelectionManager _spawnObjectSelectionManager;
    private ISpawnedObjectsManager<GameObject> _spawnedObjectsManager;

    void Start()
    {
        validateDependencies();
    }

    public void SpawnObject(Pose pose)
    {
        if (!enabled) return;

        var objectToSpawn = _spawnableARObjectsManager.GetARSpawnableObject(_spawnObjectSelectionManager.CurrentSelectedObjectIndex);
        if (objectToSpawn == null)
        {
            Logger.Log(LogSeverityLevel.Warning, $"No spawnableARObject exists with index: {_spawnObjectSelectionManager.CurrentSelectedObjectIndex}");
            return;
        }

        var spawnedObject = GameObject.Instantiate(objectToSpawn.ObjectPrefab, pose.position, pose.rotation);
        if (spawnedObject == null)
        {
            Logger.Log(LogSeverityLevel.Warning, $"Could not Instantiate the new object");
            return;
        }

        var success = _spawnedObjectsManager.AddSpawnedObject(spawnedObject);
        if (!success)
        {
            Logger.Log(LogSeverityLevel.Warning, $"Could not add the new object to the spawnedObjectsManager");
            return;
        }

        Logger.Log(LogSeverityLevel.Info, $"The number of trackedSpawnedObjects: {_spawnedObjectsManager.GetSpawnedObjectsCount()}");
    }

    private void validateDependencies()
    {
        if (spawnableARObjectsManagerReference == null)
        {
            Logger.Log(LogSeverityLevel.Error, $"Missing ISpawnableARObjectsManager reference in {nameof(SimpleARSpawnObjectSystem)}");
            enabled = false;
            return;
        }

        if (spawnObjectSelectionManagerReference == null)
        {
            Logger.Log(LogSeverityLevel.Error, $"Missing ISpawnObjectSelectionManager reference in {nameof(SimpleARSpawnObjectSystem)}");
            enabled = false;
            return;
        }

        if (spawnedObjectsManagerReference == null)
        {
            Logger.Log(LogSeverityLevel.Error, $"Missing ISpawnedObjectsManager<T> reference in {nameof(SimpleARSpawnObjectSystem)}");
            enabled = false;
            return;
        }

        if (spawnableARObjectsManagerReference is not ISpawnableARObjectsManager)
        {
            Logger.Log(LogSeverityLevel.Error, $"The ISpawnableARObjectsManager is of wrong type in {nameof(SimpleARSpawnObjectSystem)}");
            enabled = false;
            return;
        }

        if (spawnObjectSelectionManagerReference is not ISpawnObjectSelectionManager)
        {
            Logger.Log(LogSeverityLevel.Error, $"The ISpawnObjectSelectionManager is of wrong type in {nameof(SimpleARSpawnObjectSystem)}");
            enabled = false;
            return;
        }

        if (spawnedObjectsManagerReference is not ISpawnedObjectsManager<GameObject>)
        {
            Logger.Log(LogSeverityLevel.Error, $"The ISpawnedObjectsManager<T> is of wrong type in {nameof(SimpleARSpawnObjectSystem)}");
            enabled = false;
            return;
        }

        _spawnableARObjectsManager = (ISpawnableARObjectsManager)spawnableARObjectsManagerReference;
        _spawnObjectSelectionManager = (ISpawnObjectSelectionManager)spawnObjectSelectionManagerReference;
        _spawnedObjectsManager = (ISpawnedObjectsManager<GameObject>)spawnedObjectsManagerReference;
    }
}