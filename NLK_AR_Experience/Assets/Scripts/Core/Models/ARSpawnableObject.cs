using UnityEngine;

namespace NLKARExperience.Core.Models
{
    [CreateAssetMenu(menuName = "ScriptableObject/ARSpawnableObject")]
    public class ARSpawnableObject : ScriptableObject
    {
        public GameObject ObjectPrefab;
        public string Name = "Object";
    }
} 