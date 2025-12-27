using System.ComponentModel.DataAnnotations;

namespace Karne.API.Entities
{
    /// <summary>
    /// Represents a log entry in the database.
    /// Stores application events, errors, and information for debugging and auditing.
    /// </summary>
    public class AppLog
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The severity level of the log (e.g., "Info", "Warning", "Error").
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Level { get; set; } = "Info";

        /// <summary>
        /// The main message describing the event.
        /// </summary>
        [Required]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Optional: Stack trace or detailed exception information.
        /// </summary>
        public string? ExceptionDetail { get; set; }

        /// <summary>
        /// When this log was created.
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;

        /// <summary>
        /// Optional: Context about where the log came from (e.g., Controller name, User ID).
        /// </summary>
        [StringLength(100)]
        public string? Source { get; set; }
    }
}
