using NLKARExperience.Core.EventSystem.Events.App;
using NLKARExperience.Core.EventSystem.Events.Input;

namespace NLKARExperience.Core.EventSystem
{
    public class EventManager
    {
        public static readonly InputEvents InputEvent = new InputEvents();
        public static readonly AppEvents AppEvent = new AppEvents();
    }
}