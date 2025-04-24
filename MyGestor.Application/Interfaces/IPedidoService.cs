using MyGestor.Application.Dtos;

public interface IPedidoService
{
    Task<IEnumerable<PedidoDto>> ListarAsync();
    Task<PedidoDto?> ObterPorIdAsync(int id);
    Task CriarAsync(CriarPedidoDto dto);
    Task AtualizarAsync(int id, CriarPedidoDto dto);
    Task RemoverAsync(int id);
    Task<IEnumerable<PedidoDto>> BuscarRelatorioAsync(PedidoRelatorioFiltroDto filtro);

}
