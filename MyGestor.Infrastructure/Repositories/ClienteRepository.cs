using Microsoft.EntityFrameworkCore;
using MyGestor.Domain.Entities;
using MyGestor.Domain.Interfaces;
using MyGestor.Infrastructure.Persistence;

namespace MyGestor.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly AppDbContext _context;

    public ClienteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> ObterTodosAsync() =>
        await _context.Clientes.ToListAsync();

    public async Task<Cliente?> ObterPorIdAsync(int id) =>
        await _context.Clientes.FindAsync(id);

    public async Task AdicionarAsync(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Cliente cliente)
    {
        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> ExisteAsync(int clienteId)
    {
        return await _context.Clientes.AnyAsync(c => c.Id == clienteId);
    }
}
