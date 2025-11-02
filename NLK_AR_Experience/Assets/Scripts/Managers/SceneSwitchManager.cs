using NLKARExperience.Util.Enums;
using NLKARExperience.Core.EventSystem;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace NLKARExperience.Managers
{
    public class SceneSwitchManager : MonoBehaviour
    {
        void OnEnable()
        {
            EventManager.AppEvent.Scene.OnSceneSwitch.AddListener(OnSceneSwitch);
        }

        void OnDisable()
        {
            EventManager.AppEvent.Scene.OnSceneSwitch.RemoveListener(OnSceneSwitch);
        }

        private void OnSceneSwitch(AppScene newScene)
        {
            SceneManager.LoadScene((int)newScene);
        }
    }
}