
using NLKARExperience.Core.EventSystem.EventTypes;

namespace NLKARExperience.Core.EventSystem.Events.AppEvents
{
    public class DebugEvents
    {
        public readonly Event<string> LogInfo = new Event<string>();
        public readonly Event<string> LogWarning = new Event<string>();
        public readonly Event<string> LogError = new Event<string>();
    }
}

