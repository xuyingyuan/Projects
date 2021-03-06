﻿using FreshingStore.Repo.DataAccess;
using FreshingStore.Repo.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Repo.Repository
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

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        
    }
}
