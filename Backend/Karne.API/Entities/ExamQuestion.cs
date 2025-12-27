using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karne.API.Entities
{
    public class ExamQuestion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ExamId { get; set; }

        [Required]
        public int QuestionNo { get; set; }

        [Required]
        [StringLength(1)]
        public string CorrectAnswer { get; set; } = string.Empty;

        [Required]
        public int LessonId { get; set; }

        [Required]
        public int TopicId { get; set; }

        [ForeignKey("ExamId")]
        public ExamHeader Exam { get; set; } = null!;

        [ForeignKey("LessonId")]
        public Lesson Lesson { get; set; } = null!;

        [ForeignKey("TopicId")]
        public Topic Topic { get; set; } = null!;
    }
}
