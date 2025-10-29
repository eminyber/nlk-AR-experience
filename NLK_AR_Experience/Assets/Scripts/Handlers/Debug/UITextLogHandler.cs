using TMPro;
using UnityEngine;

using ILogHandler = NLKARExperience.Core.Interfaces.Handlers.ILogHandler;

namespace NLKARExperience.Handlers
{
    /// <summary>
    /// Writes log messages to a given <see cref="TextMeshProUGUI"/> element. 
    /// </summary>
    /// <remarks>
    /// This handler uses an <see cref="TextMeshProUGUI"/> to write log messages. The logger will be cleared
    /// after a defined number of log messages.
    /// <para>It requires an <see cref="TextMeshProUGUI"/> component to be located within the scene.</para>
    /// </remarks>
    public class UITextLogHandler : MonoBehaviour, ILogHandler
    {
        [Header("Text output target (TextMeshProUGUI)")]
        
        /// <summary>
        /// Cached reference to a <see cref="TextMeshProUGUI"/> element within the scene.
        /// </summary>
        [SerializeField] TextMeshProUGUI _logText;
        
        [Header("Number of lines before log is cleared")]
        
        /// <summary>
        /// The number of log messages needed before clearing the logger. 
        /// </summary>
        /// <remarks>
        /// It must be a positive number of atleast 1.
        /// </remarks>
        [SerializeField] int _maxNumberOfTextLines = 12;

        /// <summary>
        /// Validates dependencies and performs initial setup for the log display.
        /// </summary>
        /// <remarks>
        /// This method checks for two conditions:
        /// <list type="bullet">
        ///     <item>It ensures the <see cref="_logText"/> reference is not null.</item>
        ///     <item>It ensures <see cref="_maxNumberOfTextLines"/> is a positive value.</item>
        /// </list>
        /// If either dependency or constraint is invalid, the component disables itself 
        /// to prevent null reference exceptions and unwanted behavior.
        /// 
        /// If validation succeeds, the component sets the <see cref="_logText"/>'s 
        /// <see cref="UnityEngine.UI.Graphic.raycastTarget"/> property to <c>false</c> 
        /// to prevent the log display from blocking other user input.
        /// </remarks>
        void Start()
        {
            if (_logText == null || _maxNumberOfTextLines < 1)
            {
                enabled = false;
                return;
            }

            _logText.raycastTarget = false;
        }

        /// <summary>
        /// Logs a info message to the textfield <see cref="_logText"/>.
        /// </summary>
        /// <param name="message">The log message to be display</param>
        public void LogInfo(string message)
        {
            if (!enabled) return;

            clearLineIfOverflow();
            _logText.text += $"<color=\"white\">{message}</color>\n";
        }

        /// <summary>
        /// Logs a warning message to the textfield <see cref="_logText"/>.
        /// </summary>
        /// <param name="message">The log message to be display</param>
        public void LogWarning(string message)
        {
            if (!enabled) return;

            clearLineIfOverflow();
            _logText.text += $"<color=\"yellow\">{message}</color>\n";
        }

        /// <summary>
        /// Logs a error message to the textfield <see cref="_logText"/>.
        /// </summary>
        /// <param name="message">The log message to be display</param>
        public void LogError(string message)
        {
            if (!enabled) return;

            clearLineIfOverflow();
            _logText.text += $"<color=\"red\">{message}</color>\n";
        }

        /// <summary>
        /// Clears <see cref="_logText"/> if it exceeds the maximum allowed number of lines.
        /// </summary>
        /// <remarks>
        /// This method checks the current number of lines in <see cref="_logText"/>. 
        /// If the total number of lines is greater than <see cref="_maxNumberOfTextLines"/>, 
        /// the text content is cleared to prevent overflow and maintain readability.
        /// </remarks>
        private void clearLineIfOverflow()
        {
            if (_logText.text.Split('\n').Length > _maxNumberOfTextLines)
            {
                _logText.text = string.Empty;
            }
        }
    }
}