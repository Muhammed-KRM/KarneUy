using Karne.API.DTOs;
using Karne.API.Entities;

namespace Karne.API.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto request, UserRole role);
        Task<AuthResponseDto> LoginAsync(LoginDto request);
    }
}
