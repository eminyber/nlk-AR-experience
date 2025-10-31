using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Util.Enums;

using UnityEngine;

using Logger = NLKARExperience.Util.Logger;

namespace NLKARExperience.Handlers
{
    /// <summary>
    /// Interaction handler that handles the flow of interaction in the app
    /// </summary>
    /// <remarks>
    /// This class utilizes the <see cref="UIInputHandler"/>, <see cref="ObjectSelectionInputHandler"/> and <see cref="ARPlaneTouchToPoseHandler"/>
    /// to define the interaction flow of the application.
    /// <para>
    /// It requires an <see cref="UIInputHandler"/>, <see cref="ObjectSelectionInputHandler"/> and <see cref="ARPlaneTouchToPoseHandler"/> reference to
    /// work.
    /// </para>
    /// </remarks>
    public class InteractionHandler : MonoBehaviour, IUserInputHandler
    {
        //Want to add a custom property drawer so that I can use IUserInptuHandler as editor exposed seriealizeables instead
        /// <summary>
        /// Cached reference to the <see cref="UIInputHandler"/>
        /// </summary>
        [SerializeField] UIInputHandler _uiInputHandler;

        /// <summary>
        /// Cached reference to the <see cref="ObjectSelectionInputHandler"/>
        /// </summary>
        [SerializeField] ObjectSelectionInputHandler _objectSelectionHandler;

        /// <summary>
        /// Cached reference to the <see cref="ARPlaneTouchToPoseHandler"/>
        /// </summary>
        [SerializeField] ARPlaneTouchToPoseHandler _arPlaneToPoseHandler;

        /// <summary>
        /// Validates that the required dependencies exists/are valid.
        /// </summary>
        /// <remarks>
        /// If one or more reference is invalid, this component will log an error message and disable itself 
        /// to prevent null reference exceptions.
        /// </remarks>
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

        /// <summary>
        /// Controls the interaction flow of the application. 
        /// </summary>
        /// <param name="touchPosition">The position of the user's touch ón the screen</param>
        /// <returns><c>true</c> if a UI elementm selectable object or AR plane is hit, otherwise <c>false</c>.</returns>
        public bool HandleUserTouchedScreen(Vector2 touchPosition)
        {
            if (!enabled) return false;

            if (_uiInputHandler.HandleUserTouchedScreen(touchPosition))         return true;
            if (_objectSelectionHandler.HandleUserTouchedScreen(touchPosition)) return true;
            if (_arPlaneToPoseHandler.HandleUserTouchedScreen(touchPosition))   return true;

            return false;
        }
    }
}

