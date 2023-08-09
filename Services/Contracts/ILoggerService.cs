namespace Services.Contracts
{
    public interface ILoggerService
    {
        void LogDebug(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogInformation(string message);
    }
}
