using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Util.Enums;

using UnityEngine;

using Logger = NLKARExperience.Util.Logger;

namespace NLKARExperience.Handlers
{
    public class InteractionHandler : MonoBehaviour, IUserInputHandler
    {
        [SerializeField] IUserInputHandler _uiInputHandler;
        [SerializeField] IUserInputHandler _objectSelectionHandler;
        [SerializeField] IUserInputHandler _arPlaneToPoseHandler;

        void Start()
        {
            if (_uiInputHandler == null)
            {
                Logger.LogMessage(LogSeverityLevel.Error, $"Missing UIHandler reference in {nameof(InteractionHandler)}");
                enabled = false;
                return;
            }

            if (_objectSelectionHandler == null)
            {
                Logger.LogMessage(LogSeverityLevel.Error, $"Missing ObjectSelectionHandler reference in {nameof(InteractionHandler)}");
                enabled = false;
                return;
            }

            if (_arPlaneToPoseHandler == null)
            {
                Logger.LogMessage(LogSeverityLevel.Error, $"Missing ARPlaneToPoseHandler reference in {nameof(InteractionHandler)}");
                enabled = false;
                return;
            }
        }

        public bool HandleUserTouchedScreen(Vector2 touchPosition)
        {
            if (!enabled) return false;

            if (_uiInputHandler.HandleUserTouchedScreen(touchPosition)) return true;
            if (_objectSelectionHandler.HandleUserTouchedScreen(touchPosition)) return true;
            if (_arPlaneToPoseHandler.HandleUserTouchedScreen(touchPosition)) return true;

            return false;
        }
    }
}

