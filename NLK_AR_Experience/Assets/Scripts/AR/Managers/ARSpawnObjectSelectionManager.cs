using NLKARExperience.Core.Interfaces.Managers;

using UnityEngine;

namespace NLKARExperience.AR.Managers
{
    public class ARSpawnObjectSelectionManager : MonoBehaviour, ISpawnObjectSelectionManager
    {
        public int CurrentSelectedObjectIndex { get; private set; } = 0;

        public void SetCurrenSelectedObjectIndex(int index)
        {
            if (index < 0) return;

            CurrentSelectedObjectIndex = index;
        }
    }
}