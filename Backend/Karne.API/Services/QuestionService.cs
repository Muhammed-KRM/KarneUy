using Karne.API.Data;
using Karne.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Karne.API.Services
{
    public interface IQuestionService
    {
        Task<Question> CreateQuestionAsync(int userId, string content, string? imageUrl, int? lessonId, int? topicId);
        Task<List<Question>> GetQuestionsByUserAsync(int userId);
        Task<Question?> GetQuestionByIdAsync(int id);
        Task DeleteQuestionAsync(int userId, int questionId);
    }

    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext _context;

        public QuestionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Question> CreateQuestionAsync(int userId, string content, string? imageUrl, int? lessonId, int? topicId)
        {
            var question = new Question
            {
                UserId = userId,
                Content = content,
                ImageUrl = imageUrl,
                LessonId = lessonId,
                TopicId = topicId,
                CreatedAt = DateTime.Now
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task<List<Question>> GetQuestionsByUserAsync(int userId)
        {
            return await _context.Questions
                .Include(q => q.Lesson)
                .Include(q => q.Topic)
                .Where(q => q.UserId == userId)
                .OrderByDescending(q => q.CreatedAt)
                .ToListAsync();
        }

        public async Task<Question?> GetQuestionByIdAsync(int id)
        {
            return await _context.Questions
                .Include(q => q.User)
                .Include(q => q.Lesson)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task DeleteQuestionAsync(int userId, int questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);
            if (question != null && question.UserId == userId)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }
    }
}
