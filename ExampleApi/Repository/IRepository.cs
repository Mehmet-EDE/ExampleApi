using System.Linq.Expressions;

namespace ExampleApi.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>> expression);
        IQueryable<T> GetAll();
        void Add(T model);
        void Update(T model);
        void Delete(T model);

    }
}
