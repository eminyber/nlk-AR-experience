using NLKARExperience.Core.EventBus.EventData.AR;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Utils;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

public class DeleteSelectedARObjectHandler : MonoBehaviour, IEventHandler<DeleteSelectedARObjectEventData>
{
    [SerializeField] MonoBehaviour ARObjectDeletionSystemReference;

    private IDeletionSystem _arObjectDeletionSystem;

    void Start()
    {
        bool validationSucceeded = ValidateScriptDependencies();
        if (!validationSucceeded)
        {
            enabled = false;
        }
    }

    public void HandleEvent(DeleteSelectedARObjectEventData eventData)
    {
        if (!enabled) return;

        _arObjectDeletionSystem.DeleteSelectedObject();
    }

    private bool ValidateScriptDependencies()
    {
        if (!ValidateMonoDependencyUtils.ValidateDependency<IDeletionSystem>(ARObjectDeletionSystemReference, out _arObjectDeletionSystem))
        {
            Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(ARObjectDeletionSystemReference)}' does not implement or contain required dependency " +
                                               $"of type 'IDeletionSystem' in {nameof(DeleteSelectedARObjectHandler)}");
            return false;
        }

        return true;
    }
}
