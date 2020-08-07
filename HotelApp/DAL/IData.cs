using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HotelApp.DAL
{
    public interface IData<T> where T : class
    {
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null);
        Task<T> GetById(int id);
        void Create(T t);
        void Delete(T t);
        Task Delete(int id);
        void Update(T t);
        Task CommitAllRepos();
    }
}
