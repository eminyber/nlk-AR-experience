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
            SceneManager.LoadScene((int) newScene);
        }

        private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //EventBus.Publish<SceneChangedEventData>(new SceneChangedEventData());
        }
    }
}