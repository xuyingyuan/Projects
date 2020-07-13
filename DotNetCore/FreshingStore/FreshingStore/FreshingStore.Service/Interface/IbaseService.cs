using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FreshingStore.Service.Interface
{
    public interface IbaseService
    {
        void Commit();
        Task CommitAsync();

        void Dispose();

        bool ExistsProduct(int productid);
        bool ExistsProductImage(int productid, string imagetype="");
        bool ExistsProductImage(int productid, int colorid, string imagetype = "");
      
        bool ExistsProductColor(int productid);
        bool ExistsProductColor(int productid, int colorid);
        bool ExistsSku(int productid);
        bool ExistsSku(int productid, int colorid);
        bool ExistsSku(int productid, int colorid, string sizecode);



    }
}
