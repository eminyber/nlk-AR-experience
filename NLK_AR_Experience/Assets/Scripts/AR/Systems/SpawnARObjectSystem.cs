using NLKARExperience.Core.Utils;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Interfaces.Systems;
using NLKARExperience.Core.Interfaces.Managers;
using NLKARExperience.Core.Interfaces.Strategies;
using NLKARExperience.Core.Interfaces.Registries;

using System.Threading.Tasks;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Systems
{
    public class SpawnARObjectSystem : MonoBehaviour, ISpawnSystem
    {
        [Header("Spawn System Dependencies")]
        [SerializeField] MonoBehaviour SpawnableARObjectsRegistryReference;
        [SerializeField] MonoBehaviour SpawnSelectionManagerReference;
        [SerializeField] MonoBehaviour SpawnedObjectsManagerReference;

        [Header("Spawn Strategy")]
        [SerializeField] MonoBehaviour SpawnStrategyReference;

        private IObjectRegistry<int, ARSpawnableObject>  _spawnableARObjectsRegistry;
        private ISelectionManager<int> _spawnSelectionManager;
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
            bool retrivalSucceeded = RetrieveObjectToSpawn(out objectToSpawn);
            if (!retrivalSucceeded)
            {
                return;
            }

            var (instantiationSucceeded, spawnedObject) = await InstantiateObject(pose, objectToSpawn);
            if (!instantiationSucceeded)
            {
                return;
            }

            bool storeSucceeded = StoreSpawnedObject(spawnedObject);
            if (!storeSucceeded)
            {
                return;
            }
        }

        private bool StoreSpawnedObject(GameObject spawnedObject)
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

        private bool RetrieveObjectToSpawn(out ARSpawnableObject objectToSpawn)
        {
            var success = _spawnableARObjectsRegistry.TryGetObject(_spawnSelectionManager.CurrentSelected, out objectToSpawn);
            if (!success)
            {
                Logger.Log(LogSeverityLevel.Warning, $"No spawnableARObject exists with index: {_spawnSelectionManager.CurrentSelected}");
                return false;
            }

            return true;
        }

        private bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<IObjectRegistry<int, ARSpawnableObject>>(SpawnableARObjectsRegistryReference, out _spawnableARObjectsRegistry))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(SpawnableARObjectsRegistryReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IObjectRegistry<int, ARSpawnableObject>' in {nameof(SpawnARObjectSystem)}");
                return false;
            }

            if (!ValidateMonoDependencyUtils.ValidateDependency<ISelectionManager<int>>(SpawnSelectionManagerReference, out _spawnSelectionManager))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(SpawnSelectionManagerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'ISelectionManager<int>' in {nameof(SpawnARObjectSystem)}");
                return false;
            }

            if (!ValidateMonoDependencyUtils.ValidateDependency<ISpawnedObjectsManager<GameObject>>(SpawnedObjectsManagerReference, out _spawnedObjectsManager))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(SpawnedObjectsManagerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'ISpawnedObjectsManager<GameObject>' in {nameof(SpawnARObjectSystem)}");
                return false;
            }

            if (!ValidateMonoDependencyUtils.ValidateDependency<ISpawnStrategy>(SpawnStrategyReference, out _spawnStrategy))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(SpawnStrategyReference)}' does not implement or contain required dependency " +
                                                   $"of type 'ISpawnStrategy' in {nameof(SpawnARObjectSystem)}");
                return false;
            }

            return true;
        }
    }
}