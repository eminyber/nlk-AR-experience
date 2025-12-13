using NLKARExperience.Core.EventBus.EventData.System;

namespace NLKARExperience.Core.Interfaces.Utils
{
    public interface ILogger
    {
        public void Log(MessagedLoggedEventData logData);
    }
}