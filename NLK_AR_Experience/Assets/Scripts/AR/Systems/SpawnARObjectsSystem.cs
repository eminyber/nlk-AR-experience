using NLKARExperience.Core.Interfaces.Managers;
using NLKARExperience.Core.Interfaces.Registries;
using NLKARExperience.Core.Interfaces.Systems;
using NLKARExperience.Core.Interfaces.Strategies;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Utils;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;
using System.Threading.Tasks;

namespace NLKARExperience.AR.Systems
{
    public class SpawnARObjectsSystem : MonoBehaviour, ISpawnSystem
    {
        [Header("Spawn System Dependencies")]
        [SerializeField] MonoBehaviour spawnableARObjectsRegistryReference;
        [SerializeField] MonoBehaviour spawnSelectionManagerReference;
        [SerializeField] MonoBehaviour spawnedObjectsManagerReference;

        [Header("Spawn Strategy")]
        [SerializeField] MonoBehaviour spawnStrategyReference;

        private IObjectRegistry<int, ARSpawnableObject>  _spawnableARObjectsRegistry;
        private ISpawnSelectionManager _spawnSelectionManager;
        private ISpawnedObjectsManager<GameObject> _spawnedObjectsManager;

        private ISpawnStrategy _spawnStrategy;

        void Start()
        {
           bool validationSucceeded = ValidateScriptDependencies();
           if (!validationSucceeded)
            {
                enabled = false;
            } 
        }

        public async void SpawnObject(Pose pose)
        {
            if (!enabled) return;

            ARSpawnableObject objectToSpawn;
            bool retrivalSucceeded = retrieveObjectToSpawn(out objectToSpawn);
            if (!retrivalSucceeded)
            {
                return;
            }

            var (instantiationSucceeded, spawnedObject) = await InstantiateObject(pose, objectToSpawn);
            if (!instantiationSucceeded)
            {
                return;
            }

            bool storeSucceeded = storeSpawnedObject(spawnedObject);
            if (!storeSucceeded)
            {
                return;
            }
        }

        private bool storeSpawnedObject(GameObject spawnedObject)
        {
            var success = _spawnedObjectsManager.AddSpawnedObject(spawnedObject);
            if (!success)
            {
                Logger.Log(LogSeverityLevel.Warning, $"Could not add the new object to the spawnedObjectsManager");
                return false;
            }

            return true;
        }

        private async Task<(bool Success, GameObject SpawnedObject)> InstantiateObject(Pose pose, ARSpawnableObject objectToSpawn)
        {
            var spawnedObject = await _spawnStrategy.SpawnAsync(objectToSpawn.ObjectPrefab, pose);
            if (spawnedObject == null)
            {
                Logger.Log(LogSeverityLevel.Warning, "Could not instantiate the new object");
                return (false, null);
            }

            return (true, spawnedObject);
        }

        private bool retrieveObjectToSpawn(out ARSpawnableObject objectToSpawn)
        {
            var success = _spawnableARObjectsRegistry.TryGetObject(_spawnSelectionManager.CurrentSelectedObjectKey, out objectToSpawn);
            if (!success)
            {
                Logger.Log(LogSeverityLevel.Warning, $"No spawnableARObject exists with index: {_spawnSelectionManager.CurrentSelectedObjectKey}");
                return false;
            }

            return true;
        }

        private bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<IObjectRegistry<int, ARSpawnableObject>>(spawnableARObjectsRegistryReference, out _spawnableARObjectsRegistry))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(spawnableARObjectsRegistryReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IObjectRegistry<int, ARSpawnableObject>' in {nameof(SpawnARObjectsSystem)}");
                return false;
            }

            if (!ValidateMonoDependencyUtils.ValidateDependency<ISpawnSelectionManager>(spawnSelectionManagerReference, out _spawnSelectionManager))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(spawnSelectionManagerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'ISpawnSelectionManager' in {nameof(SpawnARObjectsSystem)}");
                return false;
            }

            if (!ValidateMonoDependencyUtils.ValidateDependency<ISpawnedObjectsManager<GameObject>>(spawnedObjectsManagerReference, out _spawnedObjectsManager))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(spawnedObjectsManagerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'ISpawnedObjectsManager<GameObject>' in {nameof(SpawnARObjectsSystem)}");
                return false;
            }

            if (!ValidateMonoDependencyUtils.ValidateDependency<ISpawnStrategy>(spawnStrategyReference, out _spawnStrategy))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(spawnStrategyReference)}' does not implement or contain required dependency " +
                                                   $"of type 'ISpawnStrategy' in {nameof(SpawnARObjectsSystem)}");
                return false;
            }

            return true;
        }
    }
}