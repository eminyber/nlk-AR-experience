using NLKARExperience.Core.Interfaces.Handlers.Input;
using NLKARExperience.Core.Interfaces.Models;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.Input.Handlers
{
    public class ObjectInteractionHandler : MonoBehaviour, IUserInputHandler
    {
        [SerializeField] Camera mainCamera;

        void OnEnable()
        {
            if (mainCamera == null)
                mainCamera = Camera.main;
        }

        void Start()
        {
            if (mainCamera != null) return;

            Logger.Log(LogSeverityLevel.Error, $"Missing main Camera reference in {nameof(ObjectInteractionHandler)}");
            enabled = false;
        }

        public bool ProcessInput(Vector2 touchPosition)
        {
            if (!enabled) return false;

            Ray ray = mainCamera.ScreenPointToRay(touchPosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                IInteractable selectable = hit.transform.GetComponent<IInteractable>();
                if (selectable != null)
                {
                    return true;
                }
            }

            return false;
        }

    }
}