using FreshingStore.Repo.DataAccess;
using FreshingStore.Repo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Repo.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private  IUnitOfWork _unitOfWork;

        protected  AppDBContext _dbContext;
        //public Repository(AppDBContext dbcontext)
        //{
        //    _dbContext = dbcontext;
        //}

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbContext = _unitOfWork._dbcontext;
        }

        public T Get(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }
        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }
        public void Add(T entity)
        {
             _dbContext.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            foreach (T e in entities)
                _dbContext.Set<T>().Add(e);
        }

        public void Remove(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            foreach (T e in entities)
                _dbContext.Set<T>().Remove(e);
        }



        public async Task<T> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
               
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

      
             
        public async Task AddAsync(T entity)
        {
           await _dbContext.Set<T>().AddAsync(entity);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
             foreach (T e in entities)
                await _dbContext.Set<T>().AddRangeAsync(e);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }


      

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
  
}
