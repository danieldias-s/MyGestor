using Microsoft.EntityFrameworkCore;
using MyGestor.Domain.Entities;
using MyGestor.Domain.Interfaces;
using MyGestor.Infrastructure.Persistence;

public class PedidoRepository : IPedidoRepository
{
    private readonly AppDbContext _context;

    public PedidoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pedido>> ListarAsync()
        => await _context.Pedidos
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .ToListAsync();

    public async Task<Pedido?> ObterPorIdAsync(int id)
        => await _context.Pedidos
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task AdicionarAsync(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Pedido pedido)
    {
        _context.Pedidos.Remove(pedido);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExisteAsync(int id)
        => await _context.Pedidos.AnyAsync(p => p.Id == id);

   
    public IEnumerable<Pedido> Listar()
        => _context.Pedidos
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .AsNoTracking()
            .ToList();

    public async Task<IEnumerable<Pedido>> BuscarPedidosComFiltroAsync(int ano, int mes)
    {
        var query = _context.Pedidos
            .Include(p => p.Itens).ThenInclude(i => i.Produto)
            .Include(p => p.Cliente)
            .Where(p => p.DataPedido.Year == ano && p.DataPedido.Month == mes);

        return await query.AsNoTracking().ToListAsync();
    }



}
