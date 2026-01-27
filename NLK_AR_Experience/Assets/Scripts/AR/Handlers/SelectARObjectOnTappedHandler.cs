using NLKARExperience.Core.EventBus.EventData.Input;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Utils;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

public class SelectARObjectOnTappedHandler : MonoBehaviour, IEventHandler<InteractableTappedEventData>
{
    [SerializeField] MonoBehaviour ARObjectSelectionSystemReference;

    private ISelectionSystem<Transform> _ARObjectSelectionSystem;

    void Start()
    {
        bool validationSucceeded = ValidateScriptDependencies();
        if (!validationSucceeded)
        {
            enabled = false;
        }
    }

    public void HandleEvent(InteractableTappedEventData eventData)
    {
        if (!enabled) return;

        _ARObjectSelectionSystem.ToggleTargetSelection(eventData.SelectedTransform);
    }

    private bool ValidateScriptDependencies()
    {
        if (!ValidateMonoDependencyUtils.ValidateDependency<ISelectionSystem<Transform>>(ARObjectSelectionSystemReference, out _ARObjectSelectionSystem))
        {
            Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(ARObjectSelectionSystemReference)}' does not implement or contain required dependency " +
                                               $"of type 'ISelectionSystem<Transform>' in {nameof(SelectARObjectOnTappedHandler)}");
            return false;
        }

        return true;
    }
}