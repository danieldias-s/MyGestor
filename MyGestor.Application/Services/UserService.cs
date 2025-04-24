using AutoMapper;
using MyGestor.Application.DTOs;
using MyGestor.Application.DTOs.Auth;
using MyGestor.Application.Interfaces;
using MyGestor.Domain.Entities;
using MyGestor.Domain.Interfaces;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> ListarUsuariosAsync()
    {
        var usuarios = await _userRepository.ObterTodosAsync();
        return _mapper.Map<IEnumerable<UserDto>>(usuarios);
    }
    public async Task<UserDto> ObterPorEmailAsync(string email)
    {
        var usuario = await _userRepository.ObterPorEmailAsync(email);
        return _mapper.Map<UserDto>(usuario);
    }
    public async Task<bool> RegistrarUsuarioAsync(RegisterDto dto)
    {
        var usuarioExistente = await _userRepository.ObterPorEmailAsync(dto.Email);
        if (usuarioExistente != null)
            throw new InvalidOperationException("Email já está em uso.");

        var user = _mapper.Map<User>(dto);
        user.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
        user.Role = string.IsNullOrEmpty(dto.Role) ? "Usuario" : dto.Role;

        await _userRepository.AdicionarAsync(user);
        return true;
    }
    public async Task<bool> AtualizarUsuarioAsync(int id, RegisterDto dto)
    {
        var usuario = await _userRepository.ObterPorIdAsync(id);
        if (usuario == null)
            return false;

        usuario.Nome = dto.Nome;
        usuario.Email = dto.Email;
        usuario.Role = dto.Role ?? usuario.Role;

        if (!string.IsNullOrEmpty(dto.Senha))
        {
            usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);
        }

        await _userRepository.AtualizarAsync(usuario);
        return true;
    }

    public async Task<bool> ExcluirUsuarioAsync(int id)
    {
        var usuario = await _userRepository.ObterPorIdAsync(id);
        if (usuario == null)
            return false;

        await _userRepository.RemoverAsync(id);
        return true;
    }


}
