using MyGestor.Application.DTOs.Auth;

namespace MyGestor.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponseDto> GerarTokenAsync(string email, string senha);
      
    }
}
