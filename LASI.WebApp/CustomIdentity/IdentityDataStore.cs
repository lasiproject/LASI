using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace LASI.WebApp.Persistence
{
    public abstract class IdentityStore
    {
        protected readonly IdentityErrorDescriber ErrorDescriber = new IdentityErrorDescriber();
    }
}
