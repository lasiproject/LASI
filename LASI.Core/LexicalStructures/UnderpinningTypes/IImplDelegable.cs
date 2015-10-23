using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.LexicalStructures.UnderpinningTypes
{

    public interface IRoleProviderInjectable<TRole, TInjectedRoleProvider>
        where TRole : IRoleProviderInjectable<TRole, TInjectedRoleProvider>
    {
        void InjectRoleProvider(TInjectedRoleProvider provider);
    }

    public interface IRoleProviderInjection<TRole>
            where TRole : IRoleProviderInjectable<TRole, IRoleProviderInjection<TRole>>
    {
    }
}
