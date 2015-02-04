using System;

namespace AspSixApp.Models.ApplicationUsers
{
    public class FileSystemUser : WebAppIdentityUser<Guid>
    {
        public FileSystemUser() { }

        public FileSystemUser(string userName) : base(userName) { }
    }
}