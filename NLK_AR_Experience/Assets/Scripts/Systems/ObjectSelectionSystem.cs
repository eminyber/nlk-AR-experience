using NLKARExperience.Core.EventSystem;
using NLKARExperience.Util.Enums;

using UnityEngine;

using Logger = NLKARExperience.Util.Logger;

namespace NLKARExperience.Systems
{
    public class ObjectSelectionSystem : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.InputEvent.Touch.OnObjectSelected.AddListener(handleObjectSelected);
        }

        void OnDisable()
        {
            EventManager.InputEvent.Touch.OnObjectSelected.RemoveListener(handleObjectSelected);
        }

        private void handleObjectSelected(GameObject selectedObject)
        {
            if (selectedObject == null) return;

            Logger.LogMessage(LogSeverityLevel.Info, $"GameObject selected: {selectedObject.GetInstanceID()}");
        }
    }
}