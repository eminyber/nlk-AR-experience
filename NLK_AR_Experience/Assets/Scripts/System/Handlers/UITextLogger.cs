using NLKARExperience.Core.EventBus.EventData.System;
using NLKARExperience.Core.Models;

using TMPro;
using UnityEngine;

using ILogger = NLKARExperience.Core.Interfaces.Utils.ILogger;

namespace NLKARExperience.System.Handlers
{
    public class UITextLogger : MonoBehaviour, ILogger
    {
        [Header("Text output target (TextMeshProUGUI)")]
        [SerializeField] TextMeshProUGUI _textField;

        [SerializeField] int _maxNumberOfTextLines = 12;

        void Start()
        {
            if (_textField == null || _maxNumberOfTextLines < 1)
            {
                enabled = false;
                return;
            }

            _textField.raycastTarget = false;
        }

        public void Log(MessagedLoggedEventData logData)
        {
            if (!enabled) return;

            switch (logData.SeverityLevel) { 
                case LogSeverityLevel.Info:
                    logInfo($"{logData.LoggedAt.ToString()}: {logData.Message}"); 
                    break;
                case LogSeverityLevel.Warning:
                    logWarning($"{logData.LoggedAt.ToString()}: {logData.Message}");
                    break;
                case LogSeverityLevel.Error:
                    logError($"{logData.LoggedAt.ToString()}: {logData.Message}");
                    break;
                default:
                    logError($"{logData.LoggedAt.ToString()}: {logData.Message}");
                    break;
            }
        }

        private void logInfo(string logMessage)
        {
            clearLineIfOverflow();
            _textField.text += $"<color=\"white\">{logMessage}</color>\n";
        }

        private void logWarning(string logMessage)
        {
            clearLineIfOverflow();
            _textField.text += $"<color=\"yellow\">{logMessage}</color>\n";
        }

        private void logError(string logMessage)
        {
            clearLineIfOverflow();
            _textField.text += $"<color=\"red\">{logMessage}</color>\n";
        }

        private void clearLineIfOverflow()
        {
            if (_textField.text.Split('\n').Length > _maxNumberOfTextLines)
            {
                _textField.text = string.Empty;
            }
        }
    }
}