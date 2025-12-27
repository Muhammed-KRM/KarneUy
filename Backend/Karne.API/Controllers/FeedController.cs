using Karne.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Karne.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FeedController : ControllerBase
    {
        private readonly IFeedService _feedService;

        public FeedController(IFeedService feedService)
        {
            _feedService = feedService;
        }

        [HttpGet("home")]
        public async Task<IActionResult> GetHomeFeed([FromQuery] int skip = 0, [FromQuery] int take = 20)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var feed = await _feedService.GetHomeFeedAsync(userId, skip, take);
            return Ok(feed);
        }

        [HttpGet("explore")]
        public async Task<IActionResult> GetExploreFeed([FromQuery] int take = 20)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var feed = await _feedService.GetExploreFeedAsync(userId, take);
            return Ok(feed);
        }
    }
}
