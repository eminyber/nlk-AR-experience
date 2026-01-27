using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Interfaces.Controllers;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Utils;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.UI.Handlers
{
    public class ToggleUIOnSelectedARObjectStateChangeHandler : MonoBehaviour, IEventHandler<ARObjectSelectionStateChangedEventData>
    {
        [SerializeField] MonoBehaviour ToggleUIElementControllerReference;

        private IToggleUIElementController _toggleUIElementController;

        void Start()
        {
            if (!ValidateScriptDependencies())
            {
                enabled = false;
            }
        }

        public void HandleEvent(ARObjectSelectionStateChangedEventData eventData)
        {
            if (!enabled) return;

            _toggleUIElementController.Toggle(eventData.SelectedARObject);
        }

        public bool ValidateScriptDependencies()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<IToggleUIElementController>(ToggleUIElementControllerReference, out _toggleUIElementController))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(ToggleUIElementControllerReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IToggleUIElementManager' in {nameof(ToggleUIOnSelectedARObjectStateChangeHandler)}");
                return false;
            }

            return true;
        }
    }
}