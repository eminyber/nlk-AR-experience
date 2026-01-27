using NLKARExperience.Core.Interfaces.Controllers;
using NLKARExperience.Core.Interfaces.Strategies;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Utils;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.UI.Controllers
{
    public class ARObjectSelectionUIController : MonoBehaviour, IToggleUIElementController
    {
        [SerializeField] MonoBehaviour ToggleUIElementStrategyReference;

        IToggleUIElementStrategy _toggleUIElementStrategy;

        void Start()
        {
            if (!ValidateDependency())
            {
                enabled = false;
            }
        }

        public void Toggle(Transform target)
        {
            if (!enabled) return;

            if (target == null)
            {
                _toggleUIElementStrategy.Hide();
            }
            else
            {
                _toggleUIElementStrategy.Show();
            }
        }

        public bool ValidateDependency()
        {
            if (!ValidateMonoDependencyUtils.ValidateDependency<IToggleUIElementStrategy>(ToggleUIElementStrategyReference, out _toggleUIElementStrategy))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(ToggleUIElementStrategyReference)}' does not implement or contain required dependency " +
                                                   $"of type 'IToggleUIElementStrategy' in {nameof(ARObjectSelectionUIController)}");
                return false;
            }

            return true;
        }
    }
}