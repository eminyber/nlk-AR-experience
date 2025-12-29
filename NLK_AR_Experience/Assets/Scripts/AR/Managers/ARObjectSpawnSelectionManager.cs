using NLKARExperience.Core.Interfaces.Managers;

using UnityEngine;

namespace NLKARExperience.AR.Managers
{
    public class ARObjectSpawnSelectionManager : MonoBehaviour, ISpawnSelectionManager
    {
        public int CurrentSelectedObjectKey { get; private set; } = 0;

        public void SelectObject(int index)
        {
            if (index < 0) return;

            CurrentSelectedObjectKey = index;
        }
    }
}