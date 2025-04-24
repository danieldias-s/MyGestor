using MyGestor.Domain.Entities;

namespace MyGestor.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> ObterPorEmailAsync(string email);
        Task<User?> ObterPorIdAsync(int id);
        Task<IEnumerable<User>> ObterTodosAsync();
        Task AdicionarAsync(User user);
        Task AtualizarAsync(User user);
        Task RemoverAsync(int id);
    }
}
