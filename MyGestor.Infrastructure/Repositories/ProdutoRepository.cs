using Microsoft.EntityFrameworkCore;
using MyGestor.Domain.Entities;
using MyGestor.Domain.Interfaces;
using MyGestor.Infrastructure.Persistence;

namespace MyGestor.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produto>> ObterTodosAsync() =>
        await _context.Produtos.ToListAsync();

    public async Task<Produto?> ObterPorIdAsync(int id) =>
        await _context.Produtos.FindAsync(id);

    public async Task AdicionarAsync(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Produto produto)
    {
        _context.Produtos.Update(produto);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Produto produto)
    {
        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> ExisteAsync(int produtoId)
    {
        return await _context.Produtos.AnyAsync(p => p.Id == produtoId);
    }
}
