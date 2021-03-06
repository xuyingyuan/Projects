﻿using FreshingStore.Repo.DataAccess;
using FreshingStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool ExistsProduct(int productid)
        {
            if (productid <= 1)
                return false;
            return _dbContext.Products.Where(p => p.Id == productid && p.Deleted==null).Any();
        }
         
        public bool ExistsSku(int productid)
        {
            if (productid <= 1)
                return false;
            return _dbContext.Skus.Where(p => p.Deleted == null
                                        && p.ProductId == productid).Any();
        }

        public bool ExistsSku(int productid, int colorid)
        {
            if (productid <= 1 || colorid <= 1)
                return false;
            return _dbContext.Skus.Where(p => p.Deleted == null
                                        && p.ProductId == productid
                                        && p.ColorId==colorid
                                        ).Any();
        }

        public bool ExistsSku(int productid, int colorid, string sizecode)
        {
            if (productid <= 1 || colorid <= 1)
                return false;
            return _dbContext.Skus.Where(p => p.Deleted == null
                                        && p.ProductId == productid
                                        && p.ColorId == colorid
                                        && p.SizeCode == sizecode
                                        ).Any();
        }

     

        public bool ExistsProductColor(int productid)
        {
            if (productid <= 1)
                return false;
            return _dbContext.ProductColors.Where(p => p.Deleted == null
                                       && p.ProductId == productid                                    
                                       ).Any();
        }

        public bool ExistsProductColor(int productid, int colorid)
        {
            if (productid <= 1 || colorid <= 1)
                return false;
            return _dbContext.ProductColors.Where(p => p.Deleted == null
                                       && p.ProductId == productid
                                       && p.ColorId == colorid
                                       ).Any();
        }


        public bool ExistsProductImage(int productid, int colorid, string imagetype = "")
        {
            if (productid <= 1 || colorid <= 1)
                return false;
            return _dbContext.ProductImages.Where(p => p.Deleted == null
             && p.ProductId == productid
             && p.ColorId == colorid
             && (imagetype == "" || p.ImageTypeCode == imagetype)
         ).Any();
        }
        public bool ExistsProductImage(int productid, string imagetype = "")
        {
            if (productid <= 1)
                return false;
            return _dbContext.ProductImages.Where(p => p.Deleted == null
               && p.ProductId == productid
               && (imagetype == "" || p.ImageTypeCode == imagetype)
           ).Any();
        }


       
        public bool ExistsColor(int? colorid)
        {
            if (colorid == null || colorid <= 1)
                return false;
            return _dbContext.Colors.Where(p => p.Deleted == null
                                       && p.Id == colorid
                                       ).Any();
        }
    }
}
