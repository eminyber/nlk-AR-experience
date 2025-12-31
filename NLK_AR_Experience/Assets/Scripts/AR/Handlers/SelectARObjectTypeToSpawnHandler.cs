using NLKARExperience.Core.EventBus.EventData.AR;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Interfaces.Managers;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Utils;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

public class SelectARObjectTypeToSpawnHandler : MonoBehaviour, IEventHandler<SelectSpawnTypeRequestEventData>
{
    [SerializeField] MonoBehaviour spawnObjectSelectionManagerReference;

    private ISpawnSelectionManager _spawnObjectSelectionManager;

    void Start()
    {
        bool validationSucceeded = ValidateScriptDependencies();
        if (!validationSucceeded)
        {
            enabled = false;
        }
    }

    public void HandleEvent(SelectSpawnTypeRequestEventData eventData)
    {
        if (!enabled) return;

        _spawnObjectSelectionManager.SelectObject(eventData.Index);
    }

    private bool ValidateScriptDependencies()
    {
        if (!ValidateMonoDependencyUtils.ValidateDependency<ISpawnSelectionManager>(spawnObjectSelectionManagerReference, out _spawnObjectSelectionManager))
        {
            Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(spawnObjectSelectionManagerReference)}' does not implement or contain required dependency " +
                                               $"of type 'ISpawnSelectionManager' in {nameof(SelectARObjectTypeToSpawnHandler)}");
            return false;
        }

        return true;
    }
}