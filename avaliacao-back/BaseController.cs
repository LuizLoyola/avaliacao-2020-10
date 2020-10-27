using avaliacao_back.Domain;
using avaliacao_back.Repository;
using avaliacao_back.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace avaliacao_back
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController<TService, TRepository, TModel>
        where TService : IBaseService<TRepository, TModel>
        where TRepository : IBaseRepository<TModel>
    {
        protected TService Service { get; }

        public BaseController(TService service)
        {
            Service = service;
        }

        protected static IActionResult CreateResult(object obj, int statusCode = 200)
        {
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                }),
                ContentType = "application/json",
                StatusCode = statusCode
            };
        }

        protected static IActionResult CreateError(string message, int statusCode = 500) =>
            CreateResult(new {Mensagem = message}, statusCode);
    }
}