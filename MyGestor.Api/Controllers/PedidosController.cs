using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGestor.Application.Dtos;
using MyGestor.Application.Services;

namespace MyGestor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _pedidoService;
    private readonly IMapper _mapper;

    public PedidosController(IPedidoService service, IMapper mapper)
    {
        _pedidoService = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var pedidos = await _pedidoService.ListarAsync();
        var resultado = pedidos.Select(p =>     new
        {
            p.Id,
            Cliente = p.ClienteNome,
            DataPedido = p.DataPedido.ToString("dd/MM/yyyy"), // Formato brasileiro
            Total = p.Total.ToString("C2", new CultureInfo("pt-BR")) // R$ 1.234,56
        });

        return Ok(resultado);
    }   

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Usuario")]
    public async Task<IActionResult> Get(int id)
    {
        var pedido = await _pedidoService.ObterPorIdAsync(id);
        if (pedido == null) return NotFound();
        return Ok(pedido);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Usuario")]
    public async Task<IActionResult> Post([FromBody] CriarPedidoDto dto)
    {
        await _pedidoService.CriarAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = dto.ClienteId }, dto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Put(int id, [FromBody] CriarPedidoDto dto)
    {
        await _pedidoService.AtualizarAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _pedidoService.RemoverAsync(id);
        return NoContent();
    }
   
  
    [HttpGet("relatorio")]
    public async Task<IActionResult> Relatorio([FromQuery] PedidoRelatorioFiltroDto filtro)
    {
        var resultado = await _pedidoService.BuscarRelatorioAsync(filtro);
            return Ok(resultado);
    }

}
