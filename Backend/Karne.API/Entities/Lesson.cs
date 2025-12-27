using System.ComponentModel.DataAnnotations;

namespace Karne.API.Entities
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Topic> Topics { get; set; } = new List<Topic>();
    }
}
