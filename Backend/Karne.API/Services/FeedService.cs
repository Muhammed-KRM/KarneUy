using Karne.API.Data;
using Karne.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Karne.API.Services
{
    public interface IFeedService
    {
        Task<List<Question>> GetHomeFeedAsync(int userId, int skip = 0, int take = 20);
        Task<List<Question>> GetExploreFeedAsync(int userId, int take = 20);
    }

    public class FeedService : IFeedService
    {
        private readonly ApplicationDbContext _context;

        public FeedService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetHomeFeedAsync(int userId, int skip = 0, int take = 20)
        {
            // Get list of followed user IDs
            var followingIds = await _context.SocialFollows
                .Where(f => f.FollowerId == userId)
                .Select(f => f.FollowingId)
                .ToListAsync();

            if (!followingIds.Any()) return new List<Question>();

            // Include User ID to see own posts too? Usually Home Feed includes self + following.
            followingIds.Add(userId);

            // Fetch questions
            return await _context.Questions
                .Include(q => q.User)
                .Include(q => q.Lesson)
                .Where(q => followingIds.Contains(q.UserId))
                .OrderByDescending(q => q.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<Question>> GetExploreFeedAsync(int userId, int take = 20)
        {
            // Simple logic: Get recent questions NOT from followed users (and not self)
            // Or just random/all for now since user base is small.
            
            var followingIds = await _context.SocialFollows
                .Where(f => f.FollowerId == userId)
                .Select(f => f.FollowingId)
                .ToListAsync();
                
            followingIds.Add(userId);

            return await _context.Questions
                .Include(q => q.User)
                .Where(q => !followingIds.Contains(q.UserId))
                .OrderByDescending(q => q.CreatedAt) // Or sort by Interactions count if we had it aggregated
                .Take(take)
                .ToListAsync();
        }
    }
}
