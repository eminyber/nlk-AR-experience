namespace NLKARExperience.Core.Interfaces.Managers
{
    public interface ISpawnObjectSelectionManager
    {
        int CurrentSelectedObjectIndex { get; }

        public void SetCurrenSelectedObjectIndex(int index);
    }
}

