using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using avaliacao_back.Domain;
using avaliacao_back.Repository.Interfaces;
using avaliacao_back.Domain.Models;
using avaliacao_back.Domain.ViewModel;
using avaliacao_back.Service.Interfaces;

namespace avaliacao_back.Service.Services
{
    public class UserService : BaseService<IUserRepository, User>, IUserService
    {
        public UserService(IUserRepository repository) : base(repository)
        {
        }

        public bool IsEmailTaken(string email) => Repository.GetAll().Any(u => u.Email == email);

        public UserViewModel CreateUser(UserCreationViewModel userViewModel)
        {
            var user = new User
            {
                Email = userViewModel.Email,
                Name = userViewModel.Email,
                PasswordHash = GenerateHash(userViewModel.Password),
                Phones = userViewModel.Phones,
                Token = GenerateToken()
            };

            Repository.CreateUser(user);

            return ToViewModel(user);
        }

        private static UserViewModel ToViewModel(User user) => user == null
            ? null
            : new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phones = user.Phones.Select(p => new PhoneViewModel
                {
                    Ddd = p.Ddd,
                    Number = p.Number
                }).ToList(),
                Created = user.Created,
                Modified = user.Modified,
                LastLogin = user.LastLogin,
                Token = user.Token
            };

        public UserViewModel Login(LoginViewModel loginViewModel)
        {
            var hash = GenerateHash(loginViewModel.Password);
            var user = Repository.GetAll().FirstOrDefault(u => u.Email == loginViewModel.Email && u.PasswordHash == hash);
            if (user == null) return null;
            user.LastLogin = DateTime.UtcNow;
            Repository.Update(user);
            return ToViewModel(user);
        }

        public Tuple<UserViewModel, string> CheckToken(Guid id, Guid token)
        {
            var user = Repository.GetAll().FirstOrDefault(u => u.Id == id && u.Token == token);
            if (user == null) return new Tuple<UserViewModel, string>(null, "Não autorizado");
            if (user.LastLogin.AddMinutes(30) < DateTime.UtcNow) return new Tuple<UserViewModel, string>(null, "Sessão inválida");
            return new Tuple<UserViewModel, string>(ToViewModel(user), null);
        }

        private Guid GenerateToken() => Guid.NewGuid();

        private string GenerateHash(string password)
        {
            using var sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "");
        }
    }
}