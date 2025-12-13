using NLKARExperience.Core.Interfaces.Handlers.Input;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.Input.Handlers
{
    public class SimpleInteractionHandler : MonoBehaviour, IUserInputHandler
    {
        [SerializeField] UIInteractionHandler uiInteractionHandler;
        [SerializeField] ObjectInteractionHandler objectInteractionHandler;
        [SerializeField] ARPlaneInteractionHandler arPlaneInteractionHandler;

        void Start()
        {
            if (uiInteractionHandler == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing UIInteractionHandler reference in {nameof(SimpleInteractionHandler)}");
                enabled = false;
                return;
            }

            if (objectInteractionHandler == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing ObjectInteractionHandler reference in {nameof(SimpleInteractionHandler)}");
                enabled = false;
                return;
            }

            if (arPlaneInteractionHandler == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing ARPlaneInteractionHandler reference in {nameof(SimpleInteractionHandler)}");
                enabled = false;
                return;
            }
        }

        public bool ProcessInput(Vector2 touchPosition)
        {
            if (!enabled) return false;

            if (uiInteractionHandler.ProcessInput(touchPosition)) return true;
            if (objectInteractionHandler.ProcessInput(touchPosition)) return true;
            if (arPlaneInteractionHandler.ProcessInput(touchPosition)) return true;

            return false;
        }
    }
}