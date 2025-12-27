using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karne.API.Entities
{
    public class SocialFollow
    {
        [Key]
        public int Id { get; set; }

        public int FollowerId { get; set; }
        public int FollowingId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("FollowerId")]
        public User Follower { get; set; } = null!;

        [ForeignKey("FollowingId")]
        public User Following { get; set; } = null!;
    }

    public class SocialAction
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int? QuestionId { get; set; }
        public int? TestId { get; set; }

        [Required]
        [StringLength(20)]
        public string Type { get; set; } = "Like"; // Like, Save

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        [ForeignKey("QuestionId")]
        public Question? Question { get; set; }
        [ForeignKey("TestId")]
        public Test? Test { get; set; }
    }

    public class SocialComment
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int QuestionId { get; set; }
        
        [Required]
        public string Content { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        [ForeignKey("QuestionId")]
        public Question Question { get; set; } = null!;
    }
}
