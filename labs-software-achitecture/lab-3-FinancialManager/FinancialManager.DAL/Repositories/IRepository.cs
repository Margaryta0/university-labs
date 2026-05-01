using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FinancialManager.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWithIncludes(
            params Expression<Func<T, object>>[] includes);
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
