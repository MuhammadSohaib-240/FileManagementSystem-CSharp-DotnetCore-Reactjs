using fms.Models.DTOs;
using fms.Models.Entities;
using fms.Repositories;
using fms.Security;

namespace fms.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;

        public UserService(IUserRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        public string Register(RegisterDTO dto)
        {

            var user = new User
            {
                Name = dto.Name,
                Username = dto.Username,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                UploadLimit = 100 * 1024 * 1024,
                UsedStorage = 0
            };

            _repository.Create(user);
            return "success";
        }

        public string Login(LoginDTO dto)
        {
            var user = _repository.GetByEmail(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return "Invalid Credentials";
            }

            return _jwtService.Generate(user.Id);
        }

        public User GetUserFromToken(string jwt)
        {
            var token = _jwtService.Verify(jwt);
            int userId = int.Parse(token.Issuer);
            return _repository.GetById(userId);
        }
    }
}
