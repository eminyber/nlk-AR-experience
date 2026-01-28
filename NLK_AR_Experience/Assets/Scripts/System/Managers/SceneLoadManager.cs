using NLKARExperience.Core.EventBus;
using NLKARExperience.Core.EventBus.EventData.System;
using NLKARExperience.Core.Models;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace NLKARExperience.System.Managers
{
    public class SceneLoadManager : MonoBehaviour
    {
        public static SceneLoadManager Instance { get; private set; }

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += HandleSceneLoaded;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= HandleSceneLoaded;
        }

        public void LoadScene(AppScene newScene)
        {
            //Simple for now, Should probably add confirmation etc
            if (newScene == AppScene.MainMenu)
            {
                ResetCurrentScene();
            }

            SceneManager.LoadScene((int) newScene);
        }

        private void ResetCurrentScene()
        {
            EventBus.Publish<ResetCurrentSceneRequestedEventData>(new ResetCurrentSceneRequestedEventData());
            EventBus.Publish<TurnOffARInSceneRequestedEventData>(new TurnOffARInSceneRequestedEventData());
        }

        private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //EventBus.Publish<SceneChangedEventData>(new SceneChangedEventData());
        }
    }
}