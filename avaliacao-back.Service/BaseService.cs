using System;
using avaliacao_back.Domain;
using avaliacao_back.Repository;

namespace avaliacao_back.Service
{
    public abstract class BaseService<TRepository, TModel> : IBaseService<TRepository, TModel>
        where TRepository : IBaseRepository<TModel>
    {
        protected TRepository Repository { get; }

        public BaseService(TRepository repository)
        {
            Repository = repository;
        }

        public TModel Update(TModel item) => Repository.Update(item);
        public void Delete(TModel item) => Repository.Delete(item);
    }
}