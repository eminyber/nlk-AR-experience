using NLKARExperience.Core.Models;

using System;

namespace NLKARExperience.Core.EventBus.EventData.System
{
    public readonly struct MessagedLoggedEventData
    {
        public LogSeverityLevel SeverityLevel { get; }
        public string Message { get; }

        public DateTime LoggedAt { get; }

        public MessagedLoggedEventData(LogSeverityLevel logSeverityLevel, string message)
        {
            SeverityLevel = logSeverityLevel;
            Message = message;
            LoggedAt = DateTime.Now;
        }
    }
}
