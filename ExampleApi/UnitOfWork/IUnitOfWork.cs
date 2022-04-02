using ExampleApi.DataAccess.Repository;

namespace ExampleApi.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;
        int SaveChanges();
    }
}
