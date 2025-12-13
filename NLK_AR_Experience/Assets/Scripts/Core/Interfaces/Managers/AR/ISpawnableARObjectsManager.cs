using NLKARExperience.Core.Models;
using UnityEngine;

namespace NLKARExperience.Core.Interfaces.Managers.AR
{
    public interface ISpawnableARObjectsManager 
    {
        public ARSpawnableObject GetARSpawnableObject(int id);   
    }
}

