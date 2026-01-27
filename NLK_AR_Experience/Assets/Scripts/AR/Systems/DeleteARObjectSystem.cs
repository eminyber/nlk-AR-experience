using NLKARExperience.Core.Interfaces.Managers;
using NLKARExperience.Core.Interfaces.Strategies;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Utils;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Systems
{
    public class DeleteARObjectSystem : MonoBehaviour, IDeletionSystem
    {
        [Header("Delete System Dependencies")]
        [SerializeField] MonoBehaviour SpawnedObjectsManagerReference;
        [SerializeField] MonoBehaviour SelectionSystemReference;

        [Header("Deletion Strategy")]
        [SerializeField] MonoBehaviour DeleteStrategyReference;

        private ISpawnedObjectsManager<GameObject> _spawnedObjectsManager;
        private ISelectionSystem<Transform> _selectionSystem;

        private IDeleteStrategy _deleteStrategy;

        void Start()
        {
            bool validationSucceeded = ValidateScriptDependencies();
            if (!validationSucceeded)
            {
                enabled = false;
            }
        }
        public void DeleteSelectedObjects()
        {
            if (!enabled) return;

            Transform currentSelectedObject;
            bool retrivalSucceeded = RetrieveCurrentSelectedObject(out currentSelectedObject);
            if (!retrivalSucceeded)
            {
                return;
            }

            var (removalSucceeded, gameObjectToDelete) = RetrieveGameObjectToDelete(currentSelectedObject.gameObject.GetInstanceID());
            if (!removalSucceeded) 
            {
                return;
            }
            
            bool deletionSucceded = DeleteGameObject(gameObjectToDelete);
            if (!deletionSucceded)
            {
                return;
            }

            //Reset the selectionSystem
            _selectionSystem.ClearSelected();
        }

        public void DeleteAllObjects()
        {
            throw new global::System.NotImplementedException();
        }

        private bool DeleteGameObject(GameObject objectToDelete)
        {
            if (!_deleteStrategy.Delete(objectToDelete))
            {
                Logger.Log(LogSeverityLevel.Warning, "Could not delete object");
                return false;
            }
            
            return true;
        }

        private (bool, GameObject) RetrieveGameObjectToDelete(int instanceId)
        {
            GameObject objectToDelete = _spawnedObjectsManager.RemoveSpawnedObject(instanceId);
            if (objectToDelete == null)
            {
                Logger.Log(LogSeverityLevel.Warning, "Could not retrive the object that is to be deleted");
                return (false, null);
            }

            return (true, objectToDelete);
        }

        private bool RetrieveCurrentSelectedObject(out Transform currentSelectedObject)
        {
            currentSelectedObject = _selectionSystem.GetSelected();
            if (currentSelectedObject == null)
            {
                Logger.Log(LogSeverityLevel.Warning, $"Could not retrive the current Selected Object or it is null");
                return false;
            }

            return true;
        }

        private bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<ISpawnedObjectsManager<GameObject>>(SpawnedObjectsManagerReference, out _spawnedObjectsManager))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(SpawnedObjectsManagerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'ISpawnedObjectsManager<GameObject>' in {nameof(DeleteARObjectSystem)}");
                return false;
            }

            if (!ValidateMonoDependencyUtils.ValidateDependency<ISelectionSystem<Transform>>(SelectionSystemReference, out _selectionSystem))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(SelectionSystemReference)}' does not implement or contain required dependency " +
                                                   $"of type 'ISelectionSystem<>' in {nameof(DeleteARObjectSystem)}");
                return false;
            }

            if (!ValidateMonoDependencyUtils.ValidateDependency<IDeleteStrategy>(DeleteStrategyReference, out _deleteStrategy))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(DeleteStrategyReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IDeleteStrategy' in {nameof(DeleteARObjectSystem)}");
                return false;
            }

            return true;
        }
    }
}