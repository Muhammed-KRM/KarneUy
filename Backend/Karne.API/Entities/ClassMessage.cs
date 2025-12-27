using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karne.API.Entities
{
    public class ClassMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClassroomId { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public string EncryptedContent { get; set; } = string.Empty; // Store encrypted

        public DateTime SentAt { get; set; } = DateTime.Now;

        [ForeignKey("ClassroomId")]
        public Classroom Classroom { get; set; } = null!;

        [ForeignKey("SenderId")]
        public User Sender { get; set; } = null!;
    }
}
