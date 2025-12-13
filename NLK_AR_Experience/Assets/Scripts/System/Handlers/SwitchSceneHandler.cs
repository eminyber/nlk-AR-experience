using NLKARExperience.Core.EventBus.EventData.System;
using NLKARExperience.Core.Interfaces.Handlers;
using NLKARExperience.System.Managers;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.System.Handlers
{
    public class SwitchSceneHandler : MonoBehaviour, IEventHandler<SwitchSceneRequestedEventData>
    {
        private SceneLoadManager _sceneLoadManager;
        
        void Awake()
        {
            _sceneLoadManager = GetComponent<SceneLoadManager>();
        }

        void Start()
        {
            if (_sceneLoadManager != null) return;

            Logger.Log(LogSeverityLevel.Error, $"Missing SceneLoadManager reference in {nameof(SwitchSceneHandler)}");
            enabled = false;
        }

        public void HandleEvent(SwitchSceneRequestedEventData eventData)
        {
            if (!enabled) return;

            _sceneLoadManager.LoadScene(eventData.SceneToLoad);
        }
    }
}