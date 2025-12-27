using System.ComponentModel.DataAnnotations;

namespace Karne.API.Entities
{
    public enum UserRole
    {
        Admin = 0,
        Teacher = 1,
        Student = 2
    }

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        // Optional: If Student, link to Student Record
        public int? StudentId { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
