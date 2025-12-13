using NLKARExperience.Core.Interfaces.Listeners;
using NLKARExperience.Core.EventBus.EventData.UI;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.UI.Listeners
{
    public class MenuTransitionListener : MonoBehaviour, IEventListener<MenuTransitionRequestedEventDatas>
    {
        [SerializeField] MonoBehaviour eventHandlerReference;

        private IEventHandler<MenuTransitionRequestedEventDatas> _eventHandler;
        void OnEnable()
        {
            EventBus.Register<MenuTransitionRequestedEventDatas>(this);
        }

        void Start()
        {
            if (eventHandlerReference == null)
            {
                Logger.Log(LogSeverityLevel.Error, $"Missing IEventHandler<T> reference in {nameof(MenuTransitionListener)}");
                enabled = false;
                return;
            }

            if (eventHandlerReference is not IEventHandler<MenuTransitionRequestedEventDatas>)
            {
                Logger.Log(LogSeverityLevel.Error, $"The referenced IEventHandler<T> is not if type IEventHandler<ShowAvailableARScenesData> in {nameof(MenuTransitionListener)}");
                enabled = false;
                return;
            }

            _eventHandler = (IEventHandler<MenuTransitionRequestedEventDatas>)eventHandlerReference;
        }

        void OnDisable()
        {
            EventBus.Unregister<MenuTransitionRequestedEventDatas>(this);
        }

        public void OnEvent(MenuTransitionRequestedEventDatas eventData)
        {
            _eventHandler.HandleEvent(eventData);
        }
    }
}