using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karne.API.Entities
{
    public class UserSubscription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int PlanId { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [ForeignKey("PlanId")]
        public SubscriptionPlan Plan { get; set; } = null!;
    }
}
