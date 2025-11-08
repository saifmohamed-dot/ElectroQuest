using ElectroQuest.Domain.Entities;

namespace ElectroQuest.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmailAndPassword(string email, string password);
        Task<int> CreateUser(User user);
    }
}
