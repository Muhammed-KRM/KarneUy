using Karne.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Karne.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateQuestionDto dto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var question = await _questionService.CreateQuestionAsync(userId, dto.Content, dto.ImageUrl, dto.LessonId, dto.TopicId);
            return Ok(question);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyQuestions()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var questions = await _questionService.GetQuestionsByUserAsync(userId);
            return Ok(questions);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _questionService.DeleteQuestionAsync(userId, id);
            return Ok();
        }
    }

    public class CreateQuestionDto
    {
        public string Content { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int? LessonId { get; set; }
        public int? TopicId { get; set; }
    }
}
