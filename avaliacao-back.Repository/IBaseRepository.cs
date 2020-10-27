using System;
using System.Linq;
using avaliacao_back.Domain;

namespace avaliacao_back.Repository
{
    public interface IBaseRepository<TModel>
    {
        public TModel Update(TModel item);
        public void Delete(TModel item);
        public IQueryable<TModel> GetAll();
    }
}