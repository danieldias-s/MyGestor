using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGestor.Application.DTOs.Auth;
using MyGestor.Application.Interfaces;

namespace MyGestor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var token = await _authService.GerarTokenAsync(loginDto.Email, loginDto.Senha);
            return Ok(token);
        }
        catch
        {
            return Unauthorized(new { mensagem = "Credenciais inválidas" });
        }
    }


    [HttpGet("teste")]
    [Authorize(Roles = "Admin,Usuario")]
    public IActionResult Teste() => Ok(new { msg = "Token válido!" });
}
