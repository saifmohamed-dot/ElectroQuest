using ElectroQuest.Domain.Entities;
using ElectroQuest.Domain.Repositories;

namespace ElectroQuest.Infrastructure.Users
{
    public class UserRepository : IUsersRepository
    {
        public Task<int> CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
