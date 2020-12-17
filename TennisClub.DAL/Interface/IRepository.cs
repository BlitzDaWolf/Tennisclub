using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TennisClub.DAL.Interface
{
    public interface IRepository<TEntity, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        internal TContext context { get; set; }
        internal DbSet<TEntity> dbSet { get; set; }

        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = ""
            );

        public TEntity GetByID(object id);
        public void Insert(TEntity entity);
        public void Delete(TEntity entityToDelete);
        public void Update(TEntity entityToUpdate);
        public List<TEntity> FromQuery(string query, params object[] parameters);
    }
}
