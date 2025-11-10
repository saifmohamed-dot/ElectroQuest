using AutoMapper;
using ElectroQuest.Application.Users.DTO;
using ElectroQuest.Domain.Entities;
using ElectroQuest.Domain.Repositories;

namespace ElectroQuest.Application.Users.Services
{
    
    public record UserRegisterCommand(UserRegisterDto user);
    public class UserRegisterationResult
    {
        public bool Success;
        public string ErrorMessage { get; set; } = string.Empty;
    }
    public class UserRegisterHandler
    {
        readonly IUsersRepository _usersRepository;
        readonly IMapper _mapper;
        public UserRegisterHandler(IUsersRepository repo , IMapper mapper)
        {
            _usersRepository = repo;
            _mapper = mapper;
        }
        public async Task<UserRegisterationResult> HandleAsync(UserRegisterCommand command)
        {
            var assumedFoundUser = await _usersRepository.GetUserByEmail(command.user.Email);
            if (assumedFoundUser != null)
            {
                return new UserRegisterationResult()
                {
                    Success = false,
                    ErrorMessage = "Email Already Exists !"
                };
            }
            await _usersRepository.CreateUserAsync(GenerateUserFromRegisterDto(command.user));
            return new UserRegisterationResult()
            {
                Success = true
            };
        }
        User GenerateUserFromRegisterDto(UserRegisterDto registerDto)
        {
            var user = _mapper.Map<User>(registerDto);
            user.PasswordHash = PasswordHelper.ComputeHashForPassword(registerDto.Password);
            user.CreatedAt = DateTime.UtcNow;
            return user;
        }
    }
}
