using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karne.API.Entities
{
    public class ClassStudent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClassroomId { get; set; }

        [Required]
        public int StudentUserId { get; set; } // Points to User with Student Role

        public DateTime JoinedAt { get; set; } = DateTime.Now;

        [ForeignKey("ClassroomId")]
        public Classroom Classroom { get; set; } = null!;

        [ForeignKey("StudentUserId")]
        public User StudentUser { get; set; } = null!;
    }
}
