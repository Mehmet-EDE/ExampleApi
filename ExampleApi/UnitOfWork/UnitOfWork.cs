using ExampleApi.Context;
using ExampleApi.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace ExampleApi.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DbContext _masterContext;

        public DbContext MasterContext
        {
            get
            {
                if (_masterContext == null)
                    _masterContext = new MasterContext();
                return _masterContext;
            }
            set { _masterContext = value; }
        }

        public void Dispose()
        {
            MasterContext.Dispose();
            MasterContext = null;
            GC.SuppressFinalize(this);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(MasterContext);
        }

        public int SaveChanges()
        {
            try
            {
                int result = MasterContext.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
