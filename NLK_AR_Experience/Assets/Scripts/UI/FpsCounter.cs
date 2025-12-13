using NLKARExperience.Core.Models;

using TMPro;
using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.Debug
{
    public class FpsCounter : MonoBehaviour
    {
        [SerializeField] TMP_Text fpsOutputText;

        void Start()
        {
            if (fpsOutputText != null) return;

            Logger.Log(LogSeverityLevel.Warning, $"Missing output source in {nameof(FpsCounter)}");
            enabled = false;
        }

        void Update()
        {
            fpsOutputText.text = "FPS: " + (1 / Time.deltaTime).ToString("n2");
        }
    }
}