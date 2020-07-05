using FreshingStore.Repo.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Repo.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        AppDBContext _dbcontext { get; }
        void Commit();
        Task CommitAsync();

    }
   
}
