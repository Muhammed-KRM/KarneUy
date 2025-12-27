using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karne.API.Entities
{
    public class StudentAnswer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentExamResultId { get; set; }

        [Required]
        public int QuestionNo { get; set; }

        [StringLength(1)]
        public string? GivenAnswer { get; set; }

        public bool IsCorrect { get; set; }

        [ForeignKey("StudentExamResultId")]
        public StudentExamResult StudentExamResult { get; set; } = null!;
    }
}
