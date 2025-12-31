using NLKARExperience.Core.Interfaces.Managers;
using NLKARExperience.Core.Interfaces.Strategies;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Utils;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Managers
{
    public class HighlightARObjectManager : MonoBehaviour, IHighlightManager
    {
        [SerializeField] MonoBehaviour HighlightStrategyReference;

        private ISelectionResponseStrategy _highlightStrategy;

        private Transform _currentHighlightedObject;

        void Start()
        {
            if (!ValidateDependencies())
            {
                enabled = false;
            }
        }

        public void OnHighLight(Transform transform)
        {
            if (!enabled) return;

            if (transform == null) return;

            if (transform.Equals(_currentHighlightedObject))
            {
                _highlightStrategy.OnDeselect(_currentHighlightedObject);
                _currentHighlightedObject = null;
            }
            else
            {
                _highlightStrategy.OnDeselect(_currentHighlightedObject);
                _highlightStrategy.OnSelect(transform);
                _currentHighlightedObject=transform;
            }
        }

        private bool ValidateDependencies()
        {
            if(!ValidateMonoDependencyUtils.ValidateDependency<ISelectionResponseStrategy>(HighlightStrategyReference, out _highlightStrategy))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(HighlightStrategyReference)}' does not implement or contain required dependency " +
                                                   $"of type 'ISelectionResponseStrategy' in {nameof(HighlightARObjectManager)}");
                return false;
            }

            return true;
        }
    }
}