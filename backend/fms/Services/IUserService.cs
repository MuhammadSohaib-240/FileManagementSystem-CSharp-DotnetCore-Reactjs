using fms.Models.DTOs;
using fms.Models.Entities;

namespace fms.Services
{
    public interface IUserService
    {
        string Register(RegisterDTO dto);
        string Login(LoginDTO dto);
        User GetUserFromToken(string jwt);
    }
}
