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
    }
}
