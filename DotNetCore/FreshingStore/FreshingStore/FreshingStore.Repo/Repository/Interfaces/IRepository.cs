using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Repo.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
       

        T Get(int id);      
        IEnumerable<T> GetAll();       
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate); 
        void Add(T entity);       
        void AddRange(IEnumerable<T> entities);      
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);  
        Task AddRangeAsync(IEnumerable<T> entities);
        IEnumerable<T> Find(
         Expression<Func<T, bool>> filter = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         string includeProperties = "");

    }
}
