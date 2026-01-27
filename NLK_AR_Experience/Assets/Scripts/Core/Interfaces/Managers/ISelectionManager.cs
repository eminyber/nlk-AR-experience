using UnityEngine;

namespace NLKARExperience.Core.Interfaces.Managers
{
    public interface ISelectionManager<T> 
    {
        public T CurrentSelected { get; }

        public void SetSelected(T target);

        public void RemoveSelected();

        public bool IsSelected(T target);
    }
}

