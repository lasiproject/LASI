using System.IO;

namespace LASI.WebApp.Persistence.MongoDB
{
    public class MongoDBOptions
    {
        private string dataDbPath;

        public string ApplicationBasePath { get; set; }

        public string ApplicationDatabaseName { get; set; }

        public bool CreateProcess { get; set; }

        public string DataDbPath
        {
            get { return dataDbPath; }
            set { dataDbPath = Path.Combine(ApplicationBasePath, value); }
        }

        public string InstanceUrl { get; set; }

        public string MongodExePath { get; set; }

        public string OrganizationCollectionName { get; set; }

        public string UserCollectionName { get; set; }

        public string UserDocumentCollectionName { get; set; }

        public string UserRoleCollectionName { get; set; }
    }
}