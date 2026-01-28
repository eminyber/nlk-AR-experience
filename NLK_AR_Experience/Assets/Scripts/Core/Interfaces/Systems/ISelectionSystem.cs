using UnityEngine;

public interface ISelectionSystem<T>
{
    public void ToggleTargetSelection(T target);
    public T GetSelected();
    public void SetSelected(T target);

    public bool HasSelected();
    public void ClearSelected();
}