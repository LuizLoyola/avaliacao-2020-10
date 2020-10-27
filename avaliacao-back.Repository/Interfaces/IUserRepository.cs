using avaliacao_back.Domain.Models;

namespace avaliacao_back.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        void CreateUser(User user);
    }
}