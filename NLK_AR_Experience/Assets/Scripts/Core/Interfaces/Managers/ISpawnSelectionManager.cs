namespace NLKARExperience.Core.Interfaces.Managers
{
    public interface ISpawnSelectionManager
    {
        int CurrentSelectedObjectKey { get; }

        public void SelectObject(int index);
    }
}

