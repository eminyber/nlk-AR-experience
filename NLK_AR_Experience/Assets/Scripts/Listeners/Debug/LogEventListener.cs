using NLKARExperience.Core.EventSystem;

using UnityEngine;

using ILogHandler = NLKARExperience.Core.Interfaces.Handlers.ILogHandler;

namespace NLKARExperience.Listeners
{
    /// <summary>
    /// Listens for logging events raised by <see cref="EventManager"/>
    /// </summary>
    /// <remarks>
    /// This class acts as a bridge, translating logging events retrieved from the 
    /// <see cref="EventManager"/> into a higher-level call on an <see cref="ILogHandler"/>.
    /// <para>
    /// It requires an <see cref="ILogHandler"/> component to be on the same GameObject.
    /// </para>
    /// </remarks>
    public class LogEventListener : MonoBehaviour
    {
        /// <summary>
        /// Cached reference to the log handler found on this GameObject. 
        /// </summary>
        private ILogHandler _logHandler;

        /// <summary>
        /// Caches the log handler and subscribes to the logging events.
        /// </summary>
        void OnEnable()
        {
            _logHandler = GetComponent<ILogHandler>();

            EventManager.AppEvent.Debug.LogInfo.AddListener(handleLogEvent);
            EventManager.AppEvent.Debug.LogWarning.AddListener(handleLogWarningEvent);
            EventManager.AppEvent.Debug.LogError.AddListener(handleLogErrorEvent);
        }

        /// <summary>
        /// Validates that the <see cref="ILogHandler"/> dependency was found. 
        /// </summary>
        /// <remarks>
        /// If the handler is missing, this script will disable itself to prevent null 
        /// reference exceptions.
        /// </remarks>
        void Start()
        {
            if (_logHandler != null) return;

            enabled = false;
        }

        /// <summary>
        /// Unsubscribes to the logging events.
        /// </summary>
        void OnDisable()
        {
            EventManager.AppEvent.Debug.LogInfo.RemoveListener(handleLogEvent);
            EventManager.AppEvent.Debug.LogWarning.RemoveListener(handleLogWarningEvent);
            EventManager.AppEvent.Debug.LogError.RemoveListener(handleLogErrorEvent);
        }

        /// <summary>
        /// Callback handler for the <see cref="EventManager.AppEvent.Debug.LogInfo"/> event.
        /// </summary>
        /// <remarks>
        /// This method takes a log message and passes it to the <see cref="_logHandler"/>.
        /// </remarks>
        /// <param name="message">The string holding the log message</param>
        private void handleLogEvent(string message) => _logHandler.LogInfo(message);

        /// <summary>
        /// Callback handler for the <see cref="EventManager.AppEvent.Debug.LogWarning"/> event.
        /// </summary>
        /// <remarks>
        /// This method takes a log message and passes it to the <see cref="_logHandler"/>.
        /// </remarks>
        /// <param name="message">The string holding the log message</param>
        private void handleLogWarningEvent(string message) => _logHandler.LogWarning(message);

        /// <summary>
        /// Callback handler for the <see cref="EventManager.AppEvent.Debug.LogError"/> event.
        /// </summary>
        /// <remarks>
        /// This method takes a log message and passes it to the <see cref="_logHandler"/>.
        /// </remarks>
        /// <param name="message">The string holding the log message</param>
        private void handleLogErrorEvent(string message) => _logHandler.LogError(message);
    }
}