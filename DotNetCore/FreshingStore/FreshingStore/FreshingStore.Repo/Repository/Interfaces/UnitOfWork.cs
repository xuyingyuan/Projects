using FreshingStore.Repo.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
