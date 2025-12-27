using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karne.API.Entities
{
    public class Classroom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int TeacherId { get; set; } // Points to User with Teacher Role

        [Required]
        [StringLength(10)]
        public string JoinCode { get; set; } = string.Empty; // e.g., "A1B2C"

        [ForeignKey("TeacherId")]
        public User Teacher { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<ClassStudent> Students { get; set; } = new List<ClassStudent>();
        public ICollection<ClassMessage> Messages { get; set; } = new List<ClassMessage>();
    }
}
