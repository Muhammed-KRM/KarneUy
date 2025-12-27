using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karne.API.Entities
{
    public class Test
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsPublic { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        public ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();
    }

    public class TestQuestion
    {
        [Key]
        public int Id { get; set; }
        public int TestId { get; set; }
        public int QuestionId { get; set; }
        public int Order { get; set; }

        [ForeignKey("TestId")]
        public Test Test { get; set; } = null!;
        [ForeignKey("QuestionId")]
        public Question Question { get; set; } = null!;
    }
}
