using UnityEngine;

namespace NLKARExperience.Core.Interfaces.Handlers
{
    public interface IUserInputHandler
    {
        public void HandleUserTouchedScreen(Vector2 touchPosition);
    }
}