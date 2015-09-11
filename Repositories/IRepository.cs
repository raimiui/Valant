using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Valant.Repositories
{
    public interface IRepository
    {
        IDisposable BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        void Commit();
    }

    public interface IRepository<T> : IRepository where T : class
    {
        T GetById(object id);

        void Save(T entity);

        void Delete(T entity);

        IQueryable<T> All();

        IList<T> GetAll();
    }
}
