using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karne.API.Entities
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int LessonId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        public int? ParentId { get; set; }

        [ForeignKey("LessonId")]
        public Lesson Lesson { get; set; } = null!;

        [ForeignKey("ParentId")]
        public Topic? Parent { get; set; }

        public ICollection<Topic> SubTopics { get; set; } = new List<Topic>();
    }
}
