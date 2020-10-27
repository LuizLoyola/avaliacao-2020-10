using System;
using avaliacao_back.Domain;
using avaliacao_back.Repository;

namespace avaliacao_back.Service
{
    public interface IBaseService<TRepository, TModel>
        where TRepository : IBaseRepository<TModel>
    {
        public TModel Update(TModel item);
        public void Delete(TModel item);
    }
}