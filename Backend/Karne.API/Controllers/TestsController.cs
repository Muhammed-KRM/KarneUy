using Karne.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Karne.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TestsController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestsController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTestDto dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var test = await _testService.CreateTestAsync(userId, dto.Title, dto.Description, dto.IsPublic, dto.QuestionIds);
            return Ok(test);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var test = await _testService.GetTestByIdAsync(id);
            if (test == null) return NotFound();
            return Ok(test);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyTests()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var tests = await _testService.GetTestsByUserAsync(userId);
            return Ok(tests);
        }
    }

    public class CreateTestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsPublic { get; set; } = true;
        public List<int> QuestionIds { get; set; } = new();
    }
}
