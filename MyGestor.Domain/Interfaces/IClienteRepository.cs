using MyGestor.Domain.Entities;

namespace MyGestor.Domain.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> ObterTodosAsync();
    Task<Cliente?> ObterPorIdAsync(int id);
    Task AdicionarAsync(Cliente cliente);
    Task AtualizarAsync(Cliente cliente);
    Task RemoverAsync(Cliente cliente);
    Task<bool> ExisteAsync(int clienteId);
}
