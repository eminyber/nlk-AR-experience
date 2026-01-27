using NLKARExperience.Core.Interfaces.Controllers;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Utils;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Handlers
{
    public class HighlightSelectedARObjectHandler : MonoBehaviour, IEventHandler<ARObjectSelectionStateChangedEventData>
    {
        [SerializeField] MonoBehaviour HighlightManagerReference;

        private IHighlightController _highlightManager;

        void Start()
        {
            bool validationSucceeded = ValidateScriptDependencies();
            if (!validationSucceeded)
            {
                enabled = false;
            }
        }
        
        public void HandleEvent(ARObjectSelectionStateChangedEventData eventData)
        {
            if (!enabled) return;
 
            _highlightManager.OnHighLight(eventData.SelectedARObject);
        }

        private bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<IHighlightController>(HighlightManagerReference, out _highlightManager))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(HighlightManagerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IHighlightManager' in {nameof(HighlightSelectedARObjectHandler)}");
                return false;
            }

            return true;
        }
    }
}