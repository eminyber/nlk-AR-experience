using NLKARExperience.AR.Managers;
using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.Interfaces.Managers;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Systems
{
    public class SelectARObjectSystem : MonoBehaviour, ISelectionSystem<Transform>
    {
        private ISelectionManager<Transform> _selectionManager = new SingleARObjectSelectionManager();

        public void ClearSelected()
        {
            _selectionManager.RemoveSelected();
            NotifySelectionChange();
        }

        public Transform GetSelected()
        {
            return _selectionManager.CurrentSelected;
        }

        public void SetSelected(Transform target)
        {
            if (target == null) return;

            _selectionManager.SetSelected(target);
            NotifySelectionChange();
        }

        public void ToggleTargetSelection(Transform target)
        {
            if (target == null) return;

            if (Object.Equals(target, _selectionManager.CurrentSelected))
            {
                ClearSelected();
            }
            else
            {
                SetSelected(target);
            }
        }

        private void NotifySelectionChange()
        {
            EventBus.Publish<ARObjectSelectionStateChangedEventData>(new ARObjectSelectionStateChangedEventData(_selectionManager.CurrentSelected));
        }
    }
}