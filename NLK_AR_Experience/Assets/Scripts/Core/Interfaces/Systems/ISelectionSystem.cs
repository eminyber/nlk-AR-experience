using UnityEngine;

public interface ISelectionSystem<T>
{
    public void ToggleTargetSelection(T target);

    public T GetSelected();
    public void SetSelected(T target);

    public void ClearSelected();
}