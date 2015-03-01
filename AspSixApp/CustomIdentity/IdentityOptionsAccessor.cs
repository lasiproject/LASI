namespace AspSixApp.CustomIdentity
{
    public class IdentityOptionsAccessor : Microsoft.Framework.OptionsModel.IOptions<Microsoft.AspNet.Identity.IdentityOptions>
    {
        public IdentityOptionsAccessor(Microsoft.AspNet.Identity.IdentityOptions options)
        {
            Options = options;
        }
        public Microsoft.AspNet.Identity.IdentityOptions Options { get; private set; }
        public Microsoft.AspNet.Identity.IdentityOptions GetNamedOptions(string name) => Options;
    }
}