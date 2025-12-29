using UnityEngine;

namespace NLKARExperience.Core.Utils
{
    public static class ValidateMonoDependencyUtils
    {
        public static bool ValidateDependency<T>(MonoBehaviour script, out T validatedDependency)
        {
            if (script is T dependency)
            {
                validatedDependency = dependency;
                return true;
            }

            validatedDependency = default;
            return false;
        }
    }
}