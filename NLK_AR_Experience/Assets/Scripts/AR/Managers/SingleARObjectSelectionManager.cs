using NLKARExperience.Core.Interfaces.Managers;

using UnityEngine;

namespace NLKARExperience.AR.Managers
{
    public class SingleARObjectSelectionManager : MonoBehaviour, ISelectionManager<Transform>
    {
        private Transform _currentSelected;

        public Transform CurrentSelected => _currentSelected;

        public void SetSelected(Transform target)
        {
            if (target == null) return;

            _currentSelected = target;
        }

        public bool IsSelected(Transform target)
        {
            return _currentSelected == target;
        }

        public void RemoveSelected()
        {
            _currentSelected = null;
        }
    }
}