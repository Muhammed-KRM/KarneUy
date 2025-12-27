using Karne.API.Data;
using Karne.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Karne.API.Services
{
    public interface IInteractionService
    {
        Task LikeQuestionAsync(int userId, int questionId);
        Task UnlikeQuestionAsync(int userId, int questionId);
        Task AddCommentAsync(int userId, int questionId, string content);
        Task<List<SocialComment>> GetCommentsAsync(int questionId);
    }

    public class InteractionService : IInteractionService
    {
        private readonly ApplicationDbContext _context;

        public InteractionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LikeQuestionAsync(int userId, int questionId)
        {
            var exists = await _context.SocialActions
                .AnyAsync(a => a.UserId == userId && a.QuestionId == questionId && a.Type == "Like");

            if (exists) return;

            var action = new SocialAction
            {
                UserId = userId,
                QuestionId = questionId,
                Type = "Like",
                CreatedAt = DateTime.Now
            };

            _context.SocialActions.Add(action);
            await _context.SaveChangesAsync();
        }

        public async Task UnlikeQuestionAsync(int userId, int questionId)
        {
            var action = await _context.SocialActions
                .FirstOrDefaultAsync(a => a.UserId == userId && a.QuestionId == questionId && a.Type == "Like");

            if (action != null)
            {
                _context.SocialActions.Remove(action);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddCommentAsync(int userId, int questionId, string content)
        {
            var comment = new SocialComment
            {
                UserId = userId,
                QuestionId = questionId,
                Content = content,
                CreatedAt = DateTime.Now
            };

            _context.SocialComments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SocialComment>> GetCommentsAsync(int questionId)
        {
            return await _context.SocialComments
                .Include(c => c.User)
                .Where(c => c.QuestionId == questionId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
    }
}
