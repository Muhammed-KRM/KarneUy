using Karne.API.DTOs;
using Karne.API.Entities;
using Karne.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Karne.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register/teacher")]
        public async Task<IActionResult> RegisterTeacher(RegisterDto request)
        {
            try
            {
                var response = await _authService.RegisterAsync(request, UserRole.Teacher);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        // For demo: Anyone can register as student initially or Admin adds them?
        // Let's allow student registration for now.
        [HttpPost("register/student")]
        public async Task<IActionResult> RegisterStudent(RegisterDto request)
        {
            try
            {
                var response = await _authService.RegisterAsync(request, UserRole.Student);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
             try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
