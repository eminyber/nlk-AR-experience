using NLKARExperience.Core.Interfaces.Listeners;
using NLKARExperience.Core.EventBus.EventData.System;
using NLKARExperience.Core.EventBus;

using UnityEngine;

using ILogger = NLKARExperience.Core.Interfaces.Utils.ILogger;

namespace NLKARExperience.System.Listeners
{
    public class LogListener : MonoBehaviour, IEventListener<MessagedLoggedEventData>
    {
        private ILogger _logger;

        void OnEnable()
        {
            EventBus.Register<MessagedLoggedEventData>(this);

            _logger = GetComponent<ILogger>();
        }

        void Start()
        {
            if (_logger != null) return;

            enabled = false;
        }

        void OnDisable()
        {
            EventBus.Unregister<MessagedLoggedEventData>(this);
        }

        public void OnEvent(MessagedLoggedEventData eventData)
        {
            _logger.Log(eventData);
        }
    }
}