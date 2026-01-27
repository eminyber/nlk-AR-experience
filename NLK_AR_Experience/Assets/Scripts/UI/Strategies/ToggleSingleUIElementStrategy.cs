using NLKARExperience.Core.Interfaces.Strategies;
using NLKARExperience.Core.Models;

using UnityEngine;

using Logger = NLKARExperience.Core.Utils.Logger;

public class ToggleSingleUIElementStrategy : MonoBehaviour, IToggleUIElementStrategy
{
    [SerializeField] private GameObject UIElementReference;

    void Start()
    {
        if (UIElementReference != null) return;

        Logger.Log(LogSeverityLevel.Error, $"Validation Failed: '{nameof(UIElementReference)}' can't be null");
        enabled = false;
    }

    public bool IsVisible => UIElementReference.activeSelf;

    public void Hide()
    {
        if (!enabled) return;

        UIElementReference.SetActive(false);
    }

    public void Show()
    {
        if (!enabled) return;

        UIElementReference.SetActive(true);
    }

    public void Toggle()
    {
        if (!enabled) return;

        UIElementReference.SetActive(!UIElementReference.activeSelf);
    }
}