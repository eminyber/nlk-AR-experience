using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.EventBus.EventData.System;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.Core.Interfaces.Listeners;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.System.Listeners
{
    public class SwitchSceneListener : MonoBehaviour, IEventListener<SwitchSceneRequestedEventData>
    {
        [SerializeField] MonoBehaviour sceneSwitchHandlerReference;

        private IEventHandler<SwitchSceneRequestedEventData> _sceneSwitcheventHandler;

        void OnEnable()
        {
            EventBus.Register<SwitchSceneRequestedEventData>(this);
        }

        void Start()
        {
            if (sceneSwitchHandlerReference is null)
            {
                enabled = false;
                Logger.Log(LogSeverityLevel.Error, $"Missing IEventHandler<T> reference in {nameof(SwitchSceneListener)}");
                return;
            }

            if (sceneSwitchHandlerReference is not IEventHandler<SwitchSceneRequestedEventData>)
            {
                enabled = false;
                Logger.Log(LogSeverityLevel.Error, $"The eventHandler reference is not type of {typeof(IEventHandler<SwitchSceneRequestedEventData>)} " +
                $"but {sceneSwitchHandlerReference.GetType()} in {nameof(SwitchSceneListener)}");
                return;
            }

            _sceneSwitcheventHandler = (IEventHandler<SwitchSceneRequestedEventData>) sceneSwitchHandlerReference;
        }

        void OnDisable()
        {
            EventBus.Unregister<SwitchSceneRequestedEventData>(this);
        }

        public void OnEvent(SwitchSceneRequestedEventData eventData)
        {
            _sceneSwitcheventHandler.HandleEvent(eventData);
        }
    }
}