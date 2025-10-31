using UnityEngine;

namespace NLKARExperience.Core.Interfaces.Handlers
{
    public interface IUserInputHandler
    {
        public bool HandleUserTouchedScreen(Vector2 touchPosition);
    }
}