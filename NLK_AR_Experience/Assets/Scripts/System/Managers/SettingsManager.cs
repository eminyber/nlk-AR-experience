using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger; 

namespace NLKARExperience.System.Managers 
{
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager Instance;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            ApplySettings();
        }

        public void ApplySettings()
        {
            Logger.Log(LogSeverityLevel.Info, "In ApplySettings");

            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
    }
}