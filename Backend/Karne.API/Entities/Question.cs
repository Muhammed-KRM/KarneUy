using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karne.API.Entities
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } // Creator

        [Required]
        public string Content { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public int? LessonId { get; set; }
        public int? TopicId { get; set; }
        public int Difficulty { get; set; } = 1; // 1-5

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        
        [ForeignKey("LessonId")]
        public Lesson? Lesson { get; set; }

        [ForeignKey("TopicId")]
        public Topic? Topic { get; set; }
    }
}
