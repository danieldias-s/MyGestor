
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyGestor.Application.DTOs.Produto;
using MyGestor.Application.Interfaces;
using MyGestor.Application.Services;


namespace MyGestor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _service;

        public ProdutosController(IProdutoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Usuario")]
        public async Task<IActionResult> Get() =>
        Ok(await _service.ObterTodosAsync());

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Usuario")]
        public async Task<IActionResult> Get(int id)
        {
            var produto = await _service.ObterPorIdAsync(id);
            return produto == null ? NotFound() : Ok(produto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(CreateProdutoDto dto)
        {
            await _service.AdicionarAsync(dto);
            return Ok(new { msg = "Produto criado com sucesso!" });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, CreateProdutoDto dto)
        {
          
            await _service.AtualizarAsync(id, dto);
            return Ok(new { msg = "Produto atualizado com sucesso!" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.RemoverAsync(id);
            return Ok(new { msg = "Produto removido com sucesso!" });
        }
    }
}
