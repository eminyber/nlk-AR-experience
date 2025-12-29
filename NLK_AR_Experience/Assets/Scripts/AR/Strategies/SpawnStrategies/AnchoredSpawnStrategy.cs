using NLKARExperience.Core.Interfaces.Strategies;
using NLKARExperience.Core.Models;

using System.Threading;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.XR.ARFoundation;

using Logger = NLKARExperience.Core.Utils.Logger;

namespace NLKARExperience.AR.Strategies.SpawnStrategies
{
    public class AnchoredSpawnStrategy : MonoBehaviour, ISpawnStrategy
    {
        [SerializeField] private ARAnchorManager anchorManager;

        private void Awake()
        {
            if (anchorManager != null) return;

            Logger.Log(
                LogSeverityLevel.Error,
                $"Missing ARAnchorManager reference in {nameof(AnchoredSpawnStrategy)}"
            );

            enabled = false;
        }

        public async Task<GameObject> SpawnAsync(GameObject objectPrefab, Pose spawnPose, CancellationToken cancellationToken = default)
        {
            if (!enabled)
            {
                return null;
            }

            if (objectPrefab == null)
            {
                return null;
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var result = await anchorManager.TryAddAnchorAsync(spawnPose);
            if (!result.status.IsSuccess())
            {
                Logger.Log(LogSeverityLevel.Warning, $"No AR anchor could be added in {nameof(AnchoredSpawnStrategy)}");
                return null;
            }

            var anchor = result.value;
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPose.position, spawnPose.rotation, anchor.transform);

            return spawnedObject;
        }
    }
}