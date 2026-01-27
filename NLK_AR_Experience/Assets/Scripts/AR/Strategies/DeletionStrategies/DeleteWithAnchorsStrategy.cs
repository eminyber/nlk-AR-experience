using NLKARExperience.Core.Interfaces.Strategies;
using NLKARExperience.Core.Models;
using UnityEngine;

using UnityEngine.XR.ARFoundation;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Strategies.DeleteStrategies
{
    public class DeleteWithAnchorsStrategy : MonoBehaviour, IDeleteStrategy
    {
        [SerializeField] ARAnchorManager _anchorManager;

        void Start()
        {
            if (_anchorManager != null) return;

            Logger.Log(LogSeverityLevel.Error, "Error in DeleteWithAnchorsStrategy: ARAnchorManager reference is missing");
            enabled = false;
        }

        public bool Delete(GameObject objectToDelete)
        {
            if (!enabled) return false;

            if (objectToDelete == null) return false;

            ARAnchor anchor = objectToDelete.GetComponentInParent<ARAnchor>();
            if (anchor != null)
            {
                ARAnchorManager.Destroy(anchor);
                GameObject.Destroy(objectToDelete);
                return true;
            }

            Logger.Log(LogSeverityLevel.Error, $"Could not fin anchor in object to be deleted {objectToDelete.GetInstanceID()}");
            return false;
        }
    }
}

