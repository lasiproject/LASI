using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using AspSixApp.Models.ApplicationUsers;
using System.Collections.Immutable;

namespace AspSixApp
{
    public class FileSystemAccountProvider : IDisposable, IEnumerable<FileSystemUser>
    {
        public FileSystemAccountProvider(string serverPath) {
            userDirectory = Path.Combine(serverPath, USER_DIR);
            accounts = ImmutableHashSet.CreateRange(
                from path in Directory.EnumerateFiles(userDirectory, "*.json", SearchOption.AllDirectories)
                select JsonConvert.DeserializeObject<FileSystemUser>(File.ReadAllText(path)));

            AppDomain.CurrentDomain.DomainUnload += delegate {
                //foreach (var account in accounts) {
                //    using (var writer = new StreamWriter(Path.Combine(userDirectory, account.Email + ".json"), append: false)) {
                //        writer.Write(JsonConvert.SerializeObject(account));
                //    }
                //}
            };
        }

        public void Insert(FileSystemUser account) => accounts = accounts.Add(account);

        public IEnumerator<FileSystemUser> GetEnumerator() => accounts.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private IImmutableSet<FileSystemUser> accounts;

        private const string USER_DIR = "~/App_Data/Users/";

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        private readonly string userDirectory;

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    foreach (var account in accounts) {
                        using (var writer = new StreamWriter(Path.Combine(userDirectory, account.Email + ".json"), append: false)) {
                            writer.Write(JsonConvert.SerializeObject(account));
                        }
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources. 
        // ~FileSystemAccountProvider() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose() {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}