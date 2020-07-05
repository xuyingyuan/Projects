using FreshingStore.Repo.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Repo.Repository.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        public AppDBContext _dbcontext { get; }

        public UnitOfWork(AppDBContext dBContext)
        {
            _dbcontext = dBContext;
        }
        public void Commit()
        {
            _dbcontext.SaveChanges();
        }


        public async Task CommitAsync()
        {
          await _dbcontext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
