using ElectroQuest.Application.Users.Authentication;
using ElectroQuest.Application.Users.DTO;
using ElectroQuest.Domain.Repositories;

namespace ElectroQuest.Application.Users.Services
{
    public record UserLoginQuery(UserLoginDto user);
    public class UserLoginResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
    public class UserLoginHandler
    {
        readonly IUsersRepository _usersRepository;
        readonly IAuthentication _auth;
        public UserLoginHandler(IUsersRepository userRepo, IAuthentication auth)
        {
            _usersRepository = userRepo;
            _auth = auth;
        }
        public async Task<UserLoginResult> HandleAsync(UserLoginQuery query)
        {
            var assumedFoundUser = await _usersRepository.GetUserByEmail(query.user.Email);
            if (assumedFoundUser == null)
            {
                return new UserLoginResult()
                {
                    Success = false,
                    ErrorMessage = "Incorrect username or password"
                };
            }
            bool IsMatched = PasswordHelper.VerifyPassword(assumedFoundUser.PasswordHash, query.user.Password);
            if(!IsMatched)
            {
                return new UserLoginResult()
                {
                    Success = false,
                    ErrorMessage = "Incorrect username or password"
                };
            }
            return new UserLoginResult()
            {
                Success = true,
                Token = _auth.GenerateToken(assumedFoundUser)
            };
        }
       
    }
}
