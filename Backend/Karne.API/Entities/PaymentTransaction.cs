using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Karne.API.Entities
{
    public class PaymentTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int PlanId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Success, Failed

        [StringLength(100)]
        public string ExternalTransactionId { get; set; } = string.Empty; // From Iyzico/Stripe

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [ForeignKey("PlanId")]
        public SubscriptionPlan Plan { get; set; } = null!;
    }
}
