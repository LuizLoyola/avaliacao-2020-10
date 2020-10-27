using System;
using System.Linq;
using avaliacao_back.Domain;
using avaliacao_back.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace avaliacao_back.Repository
{
    public abstract class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class
    {
        protected DatabaseContext Context { get; }

        public BaseRepository(DatabaseContext context)
        {
            Context = context;
        }

        public TModel Update(TModel item)
        {
            Context.Set<TModel>().Add(item);
            Context.SaveChanges();
            return item;
        }

        public void Delete(TModel item)
        {
            Context.Set<TModel>().Remove(item);
            Context.SaveChanges();
        }

        public IQueryable<TModel> GetAll() => Include(Context.Set<TModel>().AsQueryable());
        protected virtual IQueryable<TModel> Include(IQueryable<TModel> query) => query;
    }
}