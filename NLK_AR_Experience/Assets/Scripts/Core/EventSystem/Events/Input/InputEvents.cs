using NLKARExperience.Core.EventSystem.Events.InputEvents;

namespace NLKARExperience.Core.EventSystem.Events.Input
{
    public class InputEvents
    {
        public readonly TouchEvents Touch = new TouchEvents();
        public readonly ButtonClickEvents ButtonClick = new ButtonClickEvents();
    }
}