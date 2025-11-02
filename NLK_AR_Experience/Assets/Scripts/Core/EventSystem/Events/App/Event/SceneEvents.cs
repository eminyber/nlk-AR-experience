using NLKARExperience.Core.EventSystem.EventTypes;
using NLKARExperience.Util.Enums;

namespace NLKARExperience.Core.EventSystem.Events.AppEvents {
    public class SceneEvents
    {   
       public readonly Event<AppScene> OnSceneSwitch = new Event<AppScene>();
    }
}