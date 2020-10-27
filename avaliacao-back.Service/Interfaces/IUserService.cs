using System;
using avaliacao_back.Domain;
using avaliacao_back.Domain.Models;
using avaliacao_back.Domain.ViewModel;
using avaliacao_back.Repository.Interfaces;

namespace avaliacao_back.Service.Interfaces
{
    public interface IUserService : IBaseService<IUserRepository, User>
    {
        public bool IsEmailTaken(string email);
        public UserViewModel CreateUser(UserCreationViewModel userViewModel);
        UserViewModel Login(LoginViewModel loginViewModel);
        Tuple<UserViewModel, string> CheckToken(Guid id, Guid token);
    }
}