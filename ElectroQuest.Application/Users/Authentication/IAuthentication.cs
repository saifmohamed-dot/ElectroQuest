using ElectroQuest.Domain.Entities;

namespace ElectroQuest.Application.Users.Authentication
{
    public interface IAuthentication
    {
        string GenerateToken(User user);
    }
}
