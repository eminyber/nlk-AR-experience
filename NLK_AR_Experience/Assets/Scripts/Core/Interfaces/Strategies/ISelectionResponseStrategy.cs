using UnityEngine;

namespace NLKARExperience.Core.Interfaces.Strategies
{
    public interface ISelectionResponseStrategy
    {
        public void OnSelect(Transform target);
        public void OnDeselect(Transform target);
    }
}