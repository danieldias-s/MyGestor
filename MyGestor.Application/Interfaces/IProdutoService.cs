using MyGestor.Application.DTOs.Produto;

namespace MyGestor.Application.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoDto>> ObterTodosAsync();
    Task<ProdutoDto?> ObterPorIdAsync(int id);
    Task AdicionarAsync(CreateProdutoDto dto);
    Task AtualizarAsync(int id, CreateProdutoDto dto);
    Task RemoverAsync(int id);
}
