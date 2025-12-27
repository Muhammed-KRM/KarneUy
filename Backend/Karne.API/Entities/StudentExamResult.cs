using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karne.API.Entities
{
    public class StudentExamResult
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int ExamId { get; set; }

        public int TotalCorrect { get; set; }
        public int TotalWrong { get; set; }
        public int TotalEmpty { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal NetScore { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Score { get; set; }

        public DateTime ProcessedDate { get; set; } = DateTime.Now;

        [ForeignKey("StudentId")]
        public Student Student { get; set; } = null!;

        [ForeignKey("ExamId")]
        public ExamHeader Exam { get; set; } = null!;

        public ICollection<StudentAnswer> Answers { get; set; } = new List<StudentAnswer>();
    }
}
