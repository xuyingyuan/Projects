using RousincaShop.Admin.Data.Entities;
using RousincaShop.Admin.Data.Repositories.Interfaces;
using RousincaShop.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RousincaShop.Admin.Data.Repositories
{
    public class LookupRepository : ILookUpRepository
    {
        protected readonly RousinaDBContext _dbContext;
        private readonly IEnumerable<ImageType> _imageTypes;
        private readonly IEnumerable<Entities.Color> _colors;
        private readonly IEnumerable<SizeCode> _sizeCodes;
        public LookupRepository(RousinaDBContext dbcontext)
        {
            _dbContext = dbcontext;
            _imageTypes = _dbContext.ImageTypes.ToList();
            _colors = _dbContext.Colors.ToList();
            _sizeCodes = _dbContext.SizeCodes.ToList();

        }
        public string getImageTypeDescription(string imagetype)
        {
            return _imageTypes.Where(i => i.ImageType1 == imagetype).FirstOrDefault().TypeDescription;
        }

    
       
        public string getSizeDescription(string sizecode)
        {
            return _sizeCodes.Where(i => i.SizeCode1 == sizecode).FirstOrDefault().Name;
        }
    }
}
