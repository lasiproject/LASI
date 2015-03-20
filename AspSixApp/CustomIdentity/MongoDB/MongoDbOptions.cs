namespace AspSixApp.CustomIdentity.MongoDB
{
    public class MongoDbOptions
    {
        public string ApplicationDatabaseName { get; set; }
        public string ConnectionString { get; }
        public string MongoDbPath { get; }
        public string MongodExePath { get; }
        public string MongoFilesDirectory { get; }
    }
}