using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExampleApi.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> dbSet;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }
        public void Add(T model)
        {
            dbSet.Add(model);
        }

        public void Delete(T model)
        {
            dbSet?.Remove(model);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            var data = dbSet.Where(expression);
            return data.FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return  dbSet;
            //return (IQueryable<T>)data;
        }

        public void Update(T model)
        {
            dbSet.Attach(model);
            _dbContext.Entry(model).State = EntityState.Modified;
        }
    }
}
