using NLKARExperience.Core.Interfaces.Controllers;
using NLKARExperience.Core.Interfaces.Strategies;
using NLKARExperience.Core.Models;
using NLKARExperience.Core.Utils;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Controllers
{
    public class HighlightSelectedARObjectController : MonoBehaviour, IHighlightController
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

        public void OnHighLight(Transform target)
        {
            if (!enabled) return;

            if (target == null)
            {
                Deselect();
            }
            else
            {
                Deselect();
                Select(target);
            }
        }

        private void Deselect()
        {
            if (_currentHighlightedObject != null)
                _highlightStrategy.OnDeselect(_currentHighlightedObject);
            
            _currentHighlightedObject = null;
        }

        private void Select(Transform target)
        {
            _highlightStrategy.OnSelect(target);
            _currentHighlightedObject = target;
        }

        private bool ValidateDependencies()
        {
            if(!ValidateMonoDependencyUtils.ValidateDependency<ISelectionResponseStrategy>(HighlightStrategyReference, out _highlightStrategy))
            {
                Logger.Log(LogSeverityLevel.Error, $"Validation failed: MonoBehaviour '{nameof(HighlightStrategyReference)}' does not implement or contain required dependency " +
                                                   $"of type 'ISelectionResponseStrategy' in {nameof(HighlightSelectedARObjectController)}");
                return false;
            }

            return true;
        }
    }
}