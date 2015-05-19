namespace LASI.WebApp.CustomIdentity.MongoDB
{
    public class MongoDBOptions
    {
        public string ApplicationDatabaseName { get; set; }
        public string InstanceUrl { get; set; }
        public string MongodExePath { get; set; }
        public string DataDbPath { get; set; }
        public bool CreateProcess { get; set; }
    }
}