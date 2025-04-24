using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGestor.Application.DTOs.Auth;
using MyGestor.Application.Interfaces;
using MyGestor.Application.Services;

namespace MyGestor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("usuarios")]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> ListarUsuarios()
    {
        var usuarios = await _userService.ListarUsuariosAsync();
        return Ok(usuarios);
    }

    [HttpGet("usuario")]
    public async Task<IActionResult> GetUsuario()
    {
        var email = User.Claims.FirstOrDefault(c => c.Type.Contains("emailaddress"))?.Value;
        if (string.IsNullOrEmpty(email))
            return BadRequest("Email do usuário não encontrado.");

        var usuario = await _userService.ObterPorEmailAsync(email);
        if (usuario == null)
            return NotFound("Usuário não encontrado.");

        return Ok(usuario);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var sucesso = await _userService.RegistrarUsuarioAsync(registerDto);

            if (!sucesso)
                return BadRequest(new { mensagem = "Erro ao registrar usuário" });

            return Ok(new { mensagem = "Usuário registrado com sucesso!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensagem = "Erro interno", erro = ex.Message });
        }
    }
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AtualizarUsuario(int id, [FromBody] RegisterDto dto)
    {
        try
        {
            var atualizado = await _userService.AtualizarUsuarioAsync(id, dto);

            if (!atualizado)
                return NotFound(new { mensagem = "Usuário não encontrado" });

            return Ok(new { mensagem = "Usuário atualizado com sucesso!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { mensagem = "Erro interno", erro = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ExcluirUsuario(int id)
    {
        try
        {
            var sucesso = await _userService.ExcluirUsuarioAsync(id);

            if (!sucesso)
                return NotFound(new { mensagem = "Usuário não encontrado" });

            return Ok(new { mensagem = "Usuário excluído com sucesso!" });
        }   
        catch (Exception ex)
        {
            return StatusCode(500, new { mensagem = "Erro interno", erro = ex.Message });
        }
    }
}