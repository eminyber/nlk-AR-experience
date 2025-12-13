using NLKARExperience.Core.Interfaces.Handlers.Input;
using NLKARExperience.Core.Models;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.Input.Handlers
{
    public class UIInteractionHandler : MonoBehaviour, IUserInputHandler
    {
        [SerializeField] GraphicRaycaster graphicRaycaster;
        [SerializeField] EventSystem eventSystem;

        void OnEnable()
        {
            if (graphicRaycaster == null)
                graphicRaycaster = GetComponent<GraphicRaycaster>();

            if (eventSystem == null)
                eventSystem = GetComponent<EventSystem>();
        }

        void Start()
        {
            if (graphicRaycaster == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing GraphicRaycaster reference on {nameof(UIInteractionHandler)}.");
                enabled = false;
                return;
            }

            if (eventSystem == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing EventSystem reference on {nameof(UIInteractionHandler)}.");
                enabled = false;
                return;
            }
        }

        public bool ProcessInput(Vector2 touchPosition)
        {
            if (!enabled) return false;

            if (eventSystem.IsPointerOverGameObject()) return false;

            var newPointerEventData = new PointerEventData(eventSystem);
            newPointerEventData.position = touchPosition;

            var raycastResults = new List<RaycastResult>();
            graphicRaycaster.Raycast(newPointerEventData, raycastResults);

            return raycastResults.Count > 0;
        }
    }
}