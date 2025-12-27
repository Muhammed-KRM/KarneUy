using Karne.API.Data;
using Karne.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Karne.API.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPaymentAsync(int userId, int planId, string cardToken);
    }

    public class MockPaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MockPaymentService> _logger;

        public MockPaymentService(ApplicationDbContext context, ILogger<MockPaymentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> ProcessPaymentAsync(int userId, int planId, string cardToken)
        {
            // 1. Validate Plan
            var plan = await _context.SubscriptionPlans.FindAsync(planId);
            if (plan == null || !plan.IsActive) 
                throw new Exception("Invalid plan.");

            // 2. Mock External Call (Iyzico / Stripe)
            _logger.LogInformation($"Processing payment for User {userId}, Plan {plan.Name}, Amount {plan.Price}...");
            
            // Simulate processing delay
            await Task.Delay(500); 
            
            bool isSuccess = !cardToken.Contains("FAIL"); // Mock failure logic

            // 3. Record Transaction
            var transaction = new PaymentTransaction
            {
                UserId = userId,
                PlanId = planId,
                Amount = plan.Price,
                Status = isSuccess ? "Success" : "Failed",
                ExternalTransactionId = Guid.NewGuid().ToString()
            };

            _context.PaymentTransactions.Add(transaction);

            // 4. If success, Activate Subscription
            if (isSuccess)
            {
                // Deactivate old active subscriptions? Or allow multiple? 
                // Let's deactivate old ones for simplicity.
                var oldSubs = await _context.UserSubscriptions
                    .Where(s => s.UserId == userId && s.IsActive)
                    .ToListAsync();
                
                foreach(var sub in oldSubs) sub.IsActive = false;

                var newSub = new UserSubscription
                {
                    UserId = userId,
                    PlanId = planId,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(plan.DurationDays),
                    IsActive = true
                };
                _context.UserSubscriptions.Add(newSub);
            }

            await _context.SaveChangesAsync();
            return isSuccess;
        }
    }
}
