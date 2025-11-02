using NLKARExperience.Core.Interfaces.Handlers;

using UnityEngine;

public class ExitApplicationHandler : MonoBehaviour, IButtonClickHandler
{
    public void HandleButtonClick()
    {
        Application.Quit();
    }
}