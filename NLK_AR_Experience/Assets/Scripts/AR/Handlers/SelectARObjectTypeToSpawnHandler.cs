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

    private ISelectionManager<int> _spawnObjectSelectionManager;

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

        _spawnObjectSelectionManager.SetSelected(eventData.Index);
    }

    private bool ValidateScriptDependencies()
    {
        if (!ValidateMonoDependencyUtils.ValidateDependency<ISelectionManager<int>>(spawnObjectSelectionManagerReference, out _spawnObjectSelectionManager))
        {
            Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(spawnObjectSelectionManagerReference)}' does not implement or contain required dependency " +
                                               $"of type 'ISelectionManager<int>' in {nameof(SelectARObjectTypeToSpawnHandler)}");
            return false;
        }

        return true;
    }
}