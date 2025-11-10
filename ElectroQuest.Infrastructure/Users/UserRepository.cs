using ElectroQuest.Domain.Entities;
using ElectroQuest.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ElectroQuest.Infrastructure.Users
{
    public class UserRepository : IUsersRepository
    {
        readonly DbContext _dbContext;
        public UserRepository(DbContext db)
        {
            _dbContext = db;
        }
        public async Task<int> CreateUserAsync(User user)
        {
            await _dbContext.Set<User>().AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Set<User>().MaxAsync(us => us.Id);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.Set<User>().Where(user => user.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string passwordHash)
        {
            return await _dbContext.Set<User>().Where(usr => usr.Email == email && usr.PasswordHash == passwordHash).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _dbContext.Set<User>().Where(usr => usr.Id == id).FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
