using NLKARExperience.Core.EventSystem;
using NLKARExperience.Util.Enums;

namespace NLKARExperience.Util
{
    /// <summary>
    /// Provides a static utility for logging messages by routing them through the application's <see cref="EventManager"/>. 
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Routes a log message to the appropriate Debug event based on the specified severity.
        /// </summary>
        /// <remarks>
        /// The message sent to the event is automatically prefixed with the severity level 
        /// (e.g., "Warning: Log message text") before being raised.
        /// </remarks>
        /// <param name="severity">The severity level (Info, Warning, or Error) of the log message.</param>
        /// <param name="message">The main text content of the log.</param>
        public static void LogMessage(LogSeverityLevel severity, string message)
        {
            switch (severity)
            {
                case LogSeverityLevel.Info:
                    EventManager.AppEvent.Debug.LogInfo.RaiseEvent($"{severity}: {message}");
                    break;
                case LogSeverityLevel.Warning:
                    EventManager.AppEvent.Debug.LogWarning.RaiseEvent($"{severity}: {message}");
                    break;
                case LogSeverityLevel.Error:
                    EventManager.AppEvent.Debug.LogError.RaiseEvent($"{severity}: {message}");
                    break;
                default:
                    EventManager.AppEvent.Debug.LogError.RaiseEvent("Unknown severity level: " + message);
                    break;
            }
        }
    }
}