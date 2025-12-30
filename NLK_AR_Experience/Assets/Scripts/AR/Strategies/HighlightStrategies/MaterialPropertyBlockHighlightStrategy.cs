using NLKARExperience.Core.Interfaces.Strategies;

using UnityEngine;

namespace NLKARExperience.AR.Strategies
{
    public class MaterialPropertyBlockHighlightStrategy: MonoBehaviour, ISelectionResponseStrategy
    {
        [SerializeField] private Color highlightColor = Color.white;
        
        private MaterialPropertyBlock _mpb;

        private const string COLOR_PROPERTY = "_Color";

        private void Awake()
        {
            _mpb = new MaterialPropertyBlock();
        }

        public void OnSelect(Transform target)
        {
            if (target == null) return;

            var renderer = target.GetComponent<Renderer>();
            if (renderer == null) return;

            renderer.GetPropertyBlock(_mpb);
            _mpb.SetColor(COLOR_PROPERTY, highlightColor);
            renderer.SetPropertyBlock(_mpb);
        }

        public void OnDeselect(Transform target)
        {
            if (target == null) return;

            var renderer = target.GetComponent<Renderer>();
            if (renderer == null) return;

            renderer.GetPropertyBlock(_mpb);
            _mpb.Clear(); 
            renderer.SetPropertyBlock(_mpb);
        }
    }
}