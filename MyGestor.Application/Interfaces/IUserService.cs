using MyGestor.Application.DTOs;
using MyGestor.Application.DTOs.Auth;

namespace MyGestor.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> ListarUsuariosAsync();
        Task<UserDto> ObterPorEmailAsync(string email);
        Task<bool> RegistrarUsuarioAsync(RegisterDto dto);
        Task<bool> ExcluirUsuarioAsync(int id);
        Task<bool> AtualizarUsuarioAsync(int id, RegisterDto dto);

    }
}
