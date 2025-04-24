using Microsoft.EntityFrameworkCore;
using MyGestor.Domain.Entities;



namespace MyGestor.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Produto> Produtos => Set<Produto>();

    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<PedidoItem> PedidoItens => Set<PedidoItem>();

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<PedidoItem>()
            .Property(p => p.PrecoUnitario)
            .HasPrecision(10, 2);

       

        base.OnModelCreating(modelBuilder);
    }
}
