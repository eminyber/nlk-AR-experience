using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.EventBus.EventData.AR;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Interfaces.Listeners;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Utils;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

public class DeleteSelectedARObjectListnere : MonoBehaviour, IEventListener<DeleteSelectedARObjectEventData>
{
    [SerializeField] MonoBehaviour eventHandlerReference;

    private IEventHandler<DeleteSelectedARObjectEventData> _eventHandler;

    void OnEnable()
    {
        EventBus.Register<DeleteSelectedARObjectEventData>(this);
    }

    void Start()
    {
        bool validationSucceeded = ValidateScriptDependencies();
        if (!validationSucceeded)
        {
            enabled = false;
        }
    }

   void OnDisable()
    {
        EventBus.Unregister<DeleteSelectedARObjectEventData>(this);
    }

    public void OnEvent(DeleteSelectedARObjectEventData eventData)
    {
        _eventHandler.HandleEvent(eventData);
    }

    private bool ValidateScriptDependencies()
    {
        if (!ValidateMonoDependencyUtils.ValidateDependency<IEventHandler<DeleteSelectedARObjectEventData>>(eventHandlerReference, out _eventHandler))
        {
            Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(eventHandlerReference)}' does not implement or contain required dependency " +
                                               $"of type 'IEventHandler<DeleteSelectedARObjectEventData>' in {nameof(DeleteSelectedARObjectListnere)}");
            return false;
        }

        return true;
    }
}
