using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.MySqlServer
{
    public interface IServerBase<T> where T:class, new()
    {
        DbSqlQuery<T> SqlQuery(string sql, params object[] parameters);

        int Count();

        int Count(Expression<Func<T, bool>> whereLambda);

        IQueryable<T> GetEntity();

        IQueryable<T> GetEntity<S>(Expression<Func<T, S>> orderByLambda, bool isAsc=true);

        IQueryable<T> GetEntity<S>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda, bool isAsc);

        T Add(T entity);

        bool Update(T entity);

        bool Delete(T entity);
    }
}
