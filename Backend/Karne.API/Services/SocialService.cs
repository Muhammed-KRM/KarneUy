using Karne.API.Data;
using Karne.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Karne.API.Services
{
    public interface ISocialService
    {
        Task FollowUserAsync(int followerId, int followingId);
        Task UnfollowUserAsync(int followerId, int followingId);
        Task<List<User>> GetFollowersAsync(int userId);
        Task<List<User>> GetFollowingAsync(int userId);
    }

    public class SocialService : ISocialService
    {
        private readonly ApplicationDbContext _context;

        public SocialService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task FollowUserAsync(int followerId, int followingId)
        {
            if (followerId == followingId) throw new Exception("Cannot follow yourself.");

            var exists = await _context.SocialFollows
                .AnyAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);
            
            if (exists) return; // Already following

            var follow = new SocialFollow
            {
                FollowerId = followerId,
                FollowingId = followingId,
                CreatedAt = DateTime.Now
            };

            _context.SocialFollows.Add(follow);
            await _context.SaveChangesAsync();
        }

        public async Task UnfollowUserAsync(int followerId, int followingId)
        {
            var follow = await _context.SocialFollows
                .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowingId == followingId);

            if (follow != null)
            {
                _context.SocialFollows.Remove(follow);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetFollowersAsync(int userId)
        {
            return await _context.SocialFollows
                .Where(f => f.FollowingId == userId)
                .Select(f => f.Follower)
                .ToListAsync();
        }

        public async Task<List<User>> GetFollowingAsync(int userId)
        {
            return await _context.SocialFollows
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Following)
                .ToListAsync();
        }
    }
}
