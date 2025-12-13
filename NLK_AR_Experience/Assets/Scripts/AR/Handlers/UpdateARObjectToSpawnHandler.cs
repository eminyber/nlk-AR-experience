using NLKARExperience.Core.EventBus.EventData.AR;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Interfaces.Managers;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

public class UpdateARObjectToSpawnHandler : MonoBehaviour, IEventHandler<ARObjectToSpawnChangeRequestedEventData>
{
    [SerializeField] MonoBehaviour spawnObjectSelectionManagerReference;
    private ISpawnObjectSelectionManager _spawnObjectSelectionManager;

    void Start()
    {
        if (spawnObjectSelectionManagerReference == null)
        {
            Logger.Log(LogSeverityLevel.Error, $"Missing ICurrentARObjectSelectionManager reference in {nameof(UpdateARObjectToSpawnHandler)}");
            enabled = false;
            return;
        }

        if (spawnObjectSelectionManagerReference is not ISpawnObjectSelectionManager)
        {
            Logger.Log(LogSeverityLevel.Error, $"The referenced ICurrentARObjectSelectionManager is not of this type in {nameof(UpdateARObjectToSpawnHandler)}");
            enabled = false;
            return;
        }

        _spawnObjectSelectionManager = (ISpawnObjectSelectionManager) spawnObjectSelectionManagerReference;
    }

    public void HandleEvent(ARObjectToSpawnChangeRequestedEventData eventData)
    {
        if (!enabled) return;

        _spawnObjectSelectionManager.SetCurrenSelectedObjectIndex(eventData.Index);
    }
}