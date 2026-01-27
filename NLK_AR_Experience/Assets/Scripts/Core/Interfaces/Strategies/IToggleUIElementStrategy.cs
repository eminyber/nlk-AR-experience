namespace NLKARExperience.Core.Interfaces.Strategies
{
    public interface IToggleUIElementStrategy
    {
        public bool IsVisible { get; }
        public void Show();
        public void Hide();
        public void Toggle();
    }
}