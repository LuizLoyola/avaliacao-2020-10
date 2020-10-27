using System;
using System.Linq;
using avaliacao_back.Repository.Interfaces;
using avaliacao_back.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace avaliacao_back.Repository.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public void CreateUser(User user)
        {
            user.Created = DateTime.UtcNow;
            user.Modified = DateTime.UtcNow;
            user.LastLogin = DateTime.UtcNow;

            Update(user);
        }

        protected override IQueryable<User> Include(IQueryable<User> query)
        {
            return query.Include(u => u.Phones);
        }
    }
}