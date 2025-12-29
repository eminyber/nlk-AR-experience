using System.Threading;
using System.Threading.Tasks;

using UnityEngine;

namespace NLKARExperience.Core.Interfaces.Strategies
{
    public interface ISpawnStrategy
    {
        Task<GameObject> SpawnAsync(GameObject objectPrefab, Pose spawnPose, CancellationToken cancellationToken = default);
    }
}
