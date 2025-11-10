using ElectroQuest.Domain.Entities;

namespace ElectroQuest.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash);
        Task<User> GetUserByEmail(string email);
        Task<int> CreateUserAsync(User user);
        Task SaveChangesAsync();
    }
}
