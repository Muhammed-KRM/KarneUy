using System.ComponentModel.DataAnnotations;

namespace Karne.API.Entities
{
    public class ExamHeader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string ExamName { get; set; } = string.Empty;

        public DateTime ExamDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(1)]
        public string BookletType { get; set; } = "A";

        public ICollection<ExamQuestion> Questions { get; set; } = new List<ExamQuestion>();
    }
}
