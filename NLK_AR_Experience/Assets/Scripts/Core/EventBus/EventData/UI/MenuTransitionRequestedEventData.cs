using NLKARExperience.Core.Models;

namespace NLKARExperience.Core.EventBus.EventData.UI
{
    public readonly struct MenuTransitionRequestedEventDatas 
    {
        public readonly MenuId TransitionFrom;
        public readonly MenuId TransitionTo;

        public MenuTransitionRequestedEventDatas(MenuId transitionFrom, MenuId transitionTo)
        {
            TransitionFrom = transitionFrom;
            TransitionTo = transitionTo;
        }
    }
}