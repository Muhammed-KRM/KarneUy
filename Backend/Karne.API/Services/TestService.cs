using Karne.API.Data;
using Karne.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Karne.API.Services
{
    public interface ITestService
    {
        Task<Test> CreateTestAsync(int userId, string title, string description, bool isPublic, List<int> questionIds);
        Task<Test?> GetTestByIdAsync(int id);
        Task<List<Test>> GetTestsByUserAsync(int userId);
    }

    public class TestService : ITestService
    {
        private readonly ApplicationDbContext _context;

        public TestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Test> CreateTestAsync(int userId, string title, string description, bool isPublic, List<int> questionIds)
        {
            var test = new Test
            {
                UserId = userId,
                Title = title,
                Description = description,
                IsPublic = isPublic,
                CreatedAt = DateTime.Now
            };

            // Add Questions
            int order = 1;
            foreach (var qId in questionIds)
            {
                test.TestQuestions.Add(new TestQuestion
                {
                    QuestionId = qId,
                    Order = order++
                });
            }

            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return test;
        }

        public async Task<Test?> GetTestByIdAsync(int id)
        {
            return await _context.Tests
                .Include(t => t.User)
                .Include(t => t.TestQuestions)
                .ThenInclude(tq => tq.Question)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Test>> GetTestsByUserAsync(int userId)
        {
            return await _context.Tests
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
    }
}
