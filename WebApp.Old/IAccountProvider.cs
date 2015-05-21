using System.Collections.Generic;
using LASI.WebApp.Old.Models;

namespace LASI.WebApp.Old
{
    internal interface IAccountProvider : IEnumerable<IAccountModel>
    {
        void Insert(IAccountModel account);
    }
}