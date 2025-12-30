using NLKARExperience.Core.EventBus.EventData.Input;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Interfaces.Managers;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Utils;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Handlers
{
    public class HighlightOnInteractableObjectTappedHandler : MonoBehaviour, IEventHandler<InteractableObjectTappedEventData>
    {
        [SerializeField] MonoBehaviour HighlightManagerReference;

        IHighlightManager _highlightManager;

        void Start()
        {
            bool validationSucceeded = ValidateScriptDependencies();
            if (!validationSucceeded)
            {
                enabled = false;
            }
        }
        
        public void HandleEvent(InteractableObjectTappedEventData eventData)
        {
            if (!enabled) return;

            _highlightManager.OnHighLight(eventData.SelectedTransform);
        }

        private bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<IHighlightManager>(HighlightManagerReference, out _highlightManager))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(HighlightManagerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IHighlightManager' in {nameof(HighlightOnInteractableObjectTappedHandler)}");
                return false;
            }

            return true;
        }
    }
}