using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HotelApp.DAL.EFCore
{
    public abstract class DataEF<T> : IData<T> where T : class
    {
        protected readonly IUnitOfWork uow;
        protected readonly DbSet<T> dbSet;
        public DataEF(IUnitOfWork uow)
        {
            this.uow = uow;
            dbSet = uow.Set<T>();
        }
        public async Task CommitAllRepos() => await uow.Save();
        public void Create(T t) => dbSet.Add(t);
        public void Delete(T t) => dbSet.Remove(t);
        public async Task Delete(int id) => dbSet.Remove(await dbSet.FindAsync(id));

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null)
        {
            var query = dbSet.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetById(int id) => await dbSet.FindAsync(id);
        public void Update(T t)
        {
            dbSet.Attach(t);
        }
    }
}
