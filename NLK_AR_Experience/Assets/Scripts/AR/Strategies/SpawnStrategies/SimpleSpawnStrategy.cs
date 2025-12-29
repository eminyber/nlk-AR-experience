using NLKARExperience.Core.Interfaces.Strategies;

using System.Threading;
using System.Threading.Tasks;

using UnityEngine;

namespace NLKARExperience.AR.Strategies.SpawnStrategies
{
    public class SimpleSpawnStrategy : MonoBehaviour, ISpawnStrategy
    {
        public Task<GameObject> SpawnAsync(GameObject objectPrefab, Pose spawnPose, CancellationToken cancellationToken = default)
        {
            if (objectPrefab == null)
            {
                return Task.FromResult<GameObject>(null);
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromResult<GameObject>(null);
            }

            GameObject spawnedObject = Instantiate(objectPrefab, spawnPose.position, spawnPose.rotation);

            return Task.FromResult(spawnedObject);
        }
    }
}