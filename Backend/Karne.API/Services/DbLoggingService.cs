using Karne.API.Data;
using Karne.API.Entities;

namespace Karne.API.Services
{
    /// <summary>
    /// Implementation of ILoggingService that writes logs directly to the Database.
    /// Scoped service is recommended since it uses DbContext.
    /// </summary>
    public class DbLoggingService : ILoggingService
    {
        private readonly IServiceProvider _serviceProvider;

        // processing scope creation manually to avoid scope issues in Singleton middleware or background tasks if needed.
        // But for standard usage, we can inject DbContext directly if Scoped. 
        // Let's use IServiceProvider to create a scope for each log if we want to be safe, 
        // or just inject ApplicationDbContext if this service is Scoped.
        // "Clean Code": Let's keep it simple and Scoped.
        
        private readonly ApplicationDbContext _context;

        public DbLoggingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogInfoAsync(string message, string? source = null)
        {
            await CreateLogAsync("Info", message, null, source);
        }

        public async Task LogWarningAsync(string message, string? source = null)
        {
            await CreateLogAsync("Warning", message, null, source);
        }

        public async Task LogErrorAsync(string message, Exception? ex = null, string? source = null)
        {
            string? exceptionDetail = ex?.ToString();
            await CreateLogAsync("Error", message, exceptionDetail, source ?? ex?.Source);
        }

        private async Task CreateLogAsync(string level, string message, string? detail, string? source)
        {
            try
            {
                var log = new AppLog
                {
                    Level = level,
                    Message = message,
                    ExceptionDetail = detail,
                    Source = source,
                    Timestamp = DateTime.Now
                };

                _context.AppLogs.Add(log);
                await _context.SaveChangesAsync();
            }
            catch
            {
                // Fallback: If DB logging fails, write to Console so we don't lose it entirely.
                // We should not throw here to avoid crashing the app due to logging failure.
                Console.WriteLine($"[FATAL] Database Logging Failed! Msg: {message}");
            }
        }
    }
}
