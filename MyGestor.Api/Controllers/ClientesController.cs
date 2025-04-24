using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGestor.Application.DTOs;
using MyGestor.Application.DTOs.Cliente;
using MyGestor.Application.Interfaces;
using MyGestor.Application.Services;
using MyGestor.Domain.Entities;

namespace MyGestor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        private readonly IClienteService _clienteService;
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Usuario")]
        public async Task<IActionResult> Get() =>
        Ok(await _clienteService.ObterTodosAsync());

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Usuario")]
        public async Task<IActionResult> Get(int id)
        {
            var cliente = await _clienteService.ObterPorIdAsync(id);
            return cliente == null ? NotFound() : Ok(cliente);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Usuario")]
        public async Task<IActionResult> Post(CreateClienteDto dto)
        {
            await _clienteService.AdicionarAsync(dto);
            return Ok(new { msg = "Cliente criado com sucesso!" });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, CreateClienteDto dto)
        {
            await _clienteService.AtualizarAsync(id, dto);
            return Ok(new { msg = "Cliente atualizado com sucesso!" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _clienteService.RemoverAsync(id);
            return Ok(new { msg = "Cliente removido com sucesso!" });
        }
    }
}
