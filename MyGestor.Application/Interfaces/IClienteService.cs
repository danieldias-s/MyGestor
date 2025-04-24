using MyGestor.Application.DTOs.Cliente;

namespace MyGestor.Application.Interfaces;

public interface IClienteService
{
    Task<IEnumerable<ClienteDto>> ObterTodosAsync();
    Task<ClienteDto?> ObterPorIdAsync(int id);
    Task AdicionarAsync(CreateClienteDto dto);
    Task AtualizarAsync(int id, CreateClienteDto dto);
    Task RemoverAsync(int id);
}
