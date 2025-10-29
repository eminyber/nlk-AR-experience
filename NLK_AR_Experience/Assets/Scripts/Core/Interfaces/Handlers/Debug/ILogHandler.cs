namespace NLKARExperience.Core.Interfaces.Handlers
{
    public interface ILogHandler
    {
        public void LogInfo(string message);
        public void LogWarning(string message);
        public void LogError(string message);
    }
}