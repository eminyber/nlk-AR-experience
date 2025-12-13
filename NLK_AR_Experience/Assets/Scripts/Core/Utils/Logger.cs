using NLKARExperience.Core.EventBus.EventData.System;
using NLKARExperience.Core.Models;

namespace NLKARExperience.Core.Utils
{
    public static class Logger
    {
        public static void Log(LogSeverityLevel logSeverityLevel, string logMessage)
        {
            var logMessageData = new MessagedLoggedEventData(logSeverityLevel, logMessage);
            EventBus.EventBus.Publish<MessagedLoggedEventData>(logMessageData);
        }
    }
}