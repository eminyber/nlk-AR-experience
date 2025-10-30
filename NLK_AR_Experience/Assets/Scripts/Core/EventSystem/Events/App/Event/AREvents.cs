using NLKARExperience.Core.EventSystem.EventTypes;

namespace NLKARExperience.Core.EventSystem.Events.AppEvents 
{
    public class AREvents
    {
        public readonly SimpleEvent OnFirstARPlaneDetected = new SimpleEvent();
    }
}