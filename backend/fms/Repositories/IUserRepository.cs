using fms.Models.Entities;

namespace fms.Repositories
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetByEmail(string email);
        User GetByUsername(string username);
        User GetById(int id);
        void Update(User user);
    }
}
