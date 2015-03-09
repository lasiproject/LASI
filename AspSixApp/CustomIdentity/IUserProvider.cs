using Microsoft.AspNet.Identity;

namespace AspSixApp.CustomIdentity
{
    public interface IUserProvider<TUser> : System.Collections.Generic.IEnumerable<TUser> where TUser : class
    {
        TUser this[string id] { get; }

        IdentityResult Add(TUser user);
        IdentityResult Delete(TUser user);
        IdentityResult Update(TUser user);
    }
}