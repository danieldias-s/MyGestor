using AutoMapper;
using MyGestor.Application.DTOs.Auth;
using MyGestor.Domain.Entities;
using MyGestor.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MyGestor.Application.Interfaces;

namespace MyGestor.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _usuarioRepository;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;

    public AuthService(IUserRepository usuarioRepository, IConfiguration config, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _config = config;
        _mapper = mapper;
    }

    public async Task<TokenResponseDto> GerarTokenAsync(string email, string senha)
    {
        var usuario = await _usuarioRepository.ObterPorEmailAsync(email);
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
            throw new UnauthorizedAccessException("Credenciais inválidas");

        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new Claim(ClaimTypes.Name, usuario.Nome),
        new Claim(ClaimTypes.Email, usuario.Email),
         new Claim("role", usuario.Role)
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddHours(2);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds
        );

        return new TokenResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiraEm = expiration
        };
    }



}
