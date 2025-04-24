using MyGestor.Domain.Entities;

namespace MyGestor.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> ListarAsync();
        Task<Pedido?> ObterPorIdAsync(int id);
        Task AdicionarAsync(Pedido pedido);
        Task AtualizarAsync(Pedido pedido);
        Task RemoverAsync(Pedido pedido);
        Task<bool> ExisteAsync(int id);
        Task<IEnumerable<Pedido>> BuscarPedidosComFiltroAsync(int ano, int mes);

    }
}
