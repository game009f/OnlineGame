using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.MySqlServer.Impl
{
    public class ServiceBase<T> where T : class, new()
    {
        protected static GameServerContext db
        {
            get
            {
                return CreateDbContext();
            }
        }

        public static GameServerContext CreateDbContext()
        {
            GameServerContext dbContext = (GameServerContext)CallContext.GetData("GameServerContext");
            if (dbContext == null)
            {
                dbContext = new GameServerContext();//Model中的实体模型的EF上下文实例
                //dbContext.Database.CreateIfNotExists();//数据库不存在则自动创建 推荐在 Configuration.cs 中修改自动迁移
                CallContext.SetData("GameServerContext", dbContext);
            }
            return dbContext;
        }

        public DbSqlQuery<T> SqlQuery(string sql, params object[] parameters)
        {
            return db.Set<T>().SqlQuery(sql, parameters);
        }

        public int Count()
        {
            return db.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).Count();
        }

        public IQueryable<T> GetEntity()
        {
            return db.Set<T>();
        }

        public IQueryable<T> GetEntity<S>(Expression<Func<T, S>> orderByLambda, bool isAsc)
        {
            if (isAsc)
            {
                return db.Set<T>().OrderBy<T, S>(orderByLambda);
            }
            else
            {
                return db.Set<T>().OrderByDescending<T, S>(orderByLambda);
            }
        }

        public IQueryable<T> GetEntity<S>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda,
            bool isAsc)
        {
            if (isAsc)
            {
                return db.Set<T>().Where(whereLambda).OrderBy<T, S>(orderByLambda);
            }
            else
            {
                return db.Set<T>().Where(whereLambda).OrderByDescending<T, S>(orderByLambda);
            }
        }

        public T Add(T entity)
        {
            db.Set<T>().Add(entity);
            db.SaveChanges();
            return entity;
        }

        public bool Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public bool Delete(T entity)
        {
            db.Set<T>().Remove(entity);
            return db.SaveChanges() > 0;
        }
    }
}
