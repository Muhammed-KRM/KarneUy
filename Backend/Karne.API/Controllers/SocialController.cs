using Karne.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Karne.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SocialController : ControllerBase
    {
        private readonly ISocialService _socialService;
        private readonly IInteractionService _interactionService;

        public SocialController(ISocialService socialService, IInteractionService interactionService)
        {
            _socialService = socialService;
            _interactionService = interactionService;
        }

        // Follow Actions
        [HttpPost("follow/{userId}")]
        public async Task<IActionResult> Follow(int userId)
        {
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            try
            {
                await _socialService.FollowUserAsync(currentUserId, userId);
                return Ok(new { message = "Followed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("unfollow/{userId}")]
        public async Task<IActionResult> Unfollow(int userId)
        {
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _socialService.UnfollowUserAsync(currentUserId, userId);
            return Ok(new { message = "Unfollowed successfully" });
        }

        [HttpGet("followers")]
        public async Task<IActionResult> GetMyFollowers()
        {
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var followers = await _socialService.GetFollowersAsync(currentUserId);
            return Ok(followers);
        }

        // Question Interactions
        [HttpPost("like/question/{questionId}")]
        public async Task<IActionResult> LikeQuestion(int questionId)
        {
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _interactionService.LikeQuestionAsync(currentUserId, questionId);
            return Ok();
        }

        [HttpPost("unlike/question/{questionId}")]
        public async Task<IActionResult> UnlikeQuestion(int questionId)
        {
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _interactionService.UnlikeQuestionAsync(currentUserId, questionId);
            return Ok();
        }

        [HttpPost("comment/question/{questionId}")]
        public async Task<IActionResult> AddComment(int questionId, [FromBody] CommentDto dto)
        {
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _interactionService.AddCommentAsync(currentUserId, questionId, dto.Content);
            return Ok();
        }

        [HttpGet("comments/question/{questionId}")]
        public async Task<IActionResult> GetComments(int questionId)
        {
            var comments = await _interactionService.GetCommentsAsync(questionId);
            return Ok(comments);
        }
    }

    public class CommentDto
    {
        public string Content { get; set; } = string.Empty;
    }
}
