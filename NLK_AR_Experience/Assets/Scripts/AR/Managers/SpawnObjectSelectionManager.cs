using NLKARExperience.Core.Interfaces.Managers;

using UnityEngine;

namespace NLKARExperience.AR.Managers
{
    public class ARObjectToSpawnSelectionManager : MonoBehaviour, ISelectionManager<int>
    {
        private int _currentSelected;

        public int CurrentSelected => _currentSelected;

        public bool IsSelected(int target)
        {
            return _currentSelected == target;
        }

        public void RemoveSelected()
        {
            _currentSelected = -1;
        }

        public void SetSelected(int target)
        {
            _currentSelected = target;
        }
    }
}