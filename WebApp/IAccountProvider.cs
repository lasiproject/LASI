using System.Collections.Generic;
using LASI.WebApp.Models;

namespace LASI.WebApp
{
    internal interface IAccountProvider : IEnumerable<IAccountModel>
    {
        void Insert(AccountModel account);
    }
}