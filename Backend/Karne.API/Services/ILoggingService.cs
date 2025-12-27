namespace Karne.API.Services
{
    /// <summary>
    /// Interface for the centralized logging service.
    /// Used to abstract the logging implementation (Database, File, Console, etc.).
    /// </summary>
    public interface ILoggingService
    {
        Task LogInfoAsync(string message, string? source = null);
        Task LogWarningAsync(string message, string? source = null);
        Task LogErrorAsync(string message, Exception? ex = null, string? source = null);
    }
}
