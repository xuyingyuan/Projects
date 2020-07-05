using FreshingStore.Core.Entities;
using FreshingStore.Repo.DataAccess;
using FreshingStore.Repo.Repository.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;

namespace FreshingStore.Repo.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDBContext dbcontext) : base(dbcontext) { }
    }
}
