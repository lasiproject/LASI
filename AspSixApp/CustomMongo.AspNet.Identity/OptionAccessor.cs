using System;
using Microsoft.AspNet.Identity;
using Microsoft.Framework.OptionsModel;

namespace AspSixApp
{
    internal class OptionAccessor<T> : IOptions<IdentityOptions>
    {
        public IdentityOptions Options { get; } = new IdentityOptions();

        public IdentityOptions GetNamedOptions(string name) {
            throw new NotImplementedException();
        }
    }
}