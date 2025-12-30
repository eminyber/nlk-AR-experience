using NLKARExperience.Core.Interfaces.Strategies;

using UnityEngine;

namespace NLKARExperience.AR.Strategies
{
    public class OutlineHighlightStrategy : MonoBehaviour, ISelectionResponseStrategy
    {

        public void OnSelect(Transform target)
        {
            if (target == null) return;

            var outline = target.GetComponent<Outline>();
            if (outline == null) return;

            outline.enabled = true;
        }

        public void OnDeselect(Transform target)
        {
            if (target == null) return;

            var outline = target.GetComponent<Outline>();
            if (outline == null) return;

            outline.enabled = false;
        }
    }
}