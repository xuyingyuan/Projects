using FreshingStore.Repo.DataAccess;
using FreshingStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Service.Services
{
    public class BaseService:IbaseService
    {
        protected AppDBContext _dbContext;
        public BaseService(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
