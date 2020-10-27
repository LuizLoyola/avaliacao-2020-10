using System;
using avaliacao_back.Domain;
using avaliacao_back.Domain.Models;
using avaliacao_back.Domain.ViewModel;
using avaliacao_back.Repository.Interfaces;
using avaliacao_back.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace avaliacao_back.Controllers
{
    public class UserController : BaseController<IUserService, IUserRepository, User>
    {
        public UserController(IUserService service) : base(service)
        {
        }

        [HttpPost("cadastro")]
        public IActionResult Cadastro([FromBody] UserCreationViewModel userViewModel)
        {
            if (Service.IsEmailTaken(userViewModel.Email))
                return CreateError("E-mail já existente", 400);

            return CreateResult(Service.CreateUser(userViewModel));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            var user = Service.Login(loginViewModel);
            return user != null ? CreateResult(user) : CreateError("Usuário e/ou senha inválidos", 401);
        }

        [HttpGet("perfil/{id}")]
        public IActionResult Perfil(Guid id, [FromHeader(Name = "Authorization")] string authorization)
        {
            if (string.IsNullOrWhiteSpace(authorization)) return CreateError("Não autorizado");
            var token = new Guid(authorization.Substring(7));

            var (user, error) = Service.CheckToken(id, token);
            return user == null ? CreateError(error, 401) : CreateResult(user);
        }
    }
}