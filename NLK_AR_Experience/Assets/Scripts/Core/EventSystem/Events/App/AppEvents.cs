using NLKARExperience.Core.EventSystem.Events.AppEvents;

namespace NLKARExperience.Core.EventSystem.Events.App
{
    public class AppEvents
    {
        public readonly DebugEvents Debug = new DebugEvents();
        public readonly AREvents AR = new AREvents();
        public readonly SceneEvents Scene = new SceneEvents();
    }
}