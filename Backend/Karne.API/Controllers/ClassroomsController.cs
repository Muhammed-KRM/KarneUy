using Karne.API.DTOs;
using Karne.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Karne.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requires Login
    public class ClassroomsController : ControllerBase
    {
        private readonly IClassroomService _classroomService;

        public ClassroomsController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher,Admin")]
        public async Task<IActionResult> CreateClass([FromBody] CreateClassDto request)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _classroomService.CreateClassAsync(userId, request.Name);
            return Ok(result);
        }

        [HttpPost("join")]
        public async Task<IActionResult> JoinClass([FromBody] JoinClassDto request)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            try
            {
                await _classroomService.JoinClassAsync(userId, request.JoinCode);
                return Ok("Joined successfully.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{classId}/messages")]
        public async Task<IActionResult> SendMessage(int classId, [FromBody] SendMessageDto request)
        {
            request.ClassroomId = classId;
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _classroomService.SendMessageAsync(userId, request);
            return Ok();
        }

        [HttpGet("{classId}/messages")]
        public async Task<IActionResult> GetMessages(int classId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _classroomService.GetMessagesAsync(classId, userId);
            return Ok(result);
        }
    }
}
