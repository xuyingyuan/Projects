using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RousincaShop.Admin.Service
{
    public interface IloggerService
    {
        void LogInformation(string message, params object[] parameters);
    }
}
