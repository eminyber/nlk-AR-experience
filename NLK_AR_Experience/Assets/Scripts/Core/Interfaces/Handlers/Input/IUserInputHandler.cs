using UnityEngine;

namespace NLKARExperience.Core.Interfaces.Handlers.Input
{
    public interface IUserInputHandler
    {
        public bool ProcessInput(Vector2 touchPosition);
    }
}