using System.ComponentModel.DataAnnotations;

namespace Karne.API.Entities
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string StudentNumber { get; set; } = string.Empty;

        [StringLength(100)]
        public string? FullName { get; set; }
    }
}
