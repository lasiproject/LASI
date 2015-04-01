namespace AspSixApp.CustomIdentity.MongoDB
{
    public class MongoDbOptions
    {
        public string ApplicationDatabaseName { get; set; }
        public string InstanceUrl { get; set; }
        public string MongodExePath { get; set; }
        public string DataDbPath { get; set; }
        public bool CreateProcess { get; set; }
    }
}