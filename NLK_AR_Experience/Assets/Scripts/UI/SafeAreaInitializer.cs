using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.UI
{
    public class SafeAreaInitializer : MonoBehaviour
    {
        [SerializeField] RectTransform panel;

        void Awake()
        {
            if (panel != null)
                applySafeArea();
        }

        void Start()
        {
            if (panel != null) return;

            Logger.Log(LogSeverityLevel.Error, $"Missing RectTransform reference in {nameof(SafeAreaInitializer)}");
            enabled = false;
        }

       private void applySafeArea()
        {
            Rect safeArea = Screen.safeArea;

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            panel.anchorMin = anchorMin;
            panel.anchorMax = anchorMax;
        }
    }
}

