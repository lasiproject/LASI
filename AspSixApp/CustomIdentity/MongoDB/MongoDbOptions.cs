namespace AspSixApp.CustomIdentity.MongoDB
{
    public class MongoDbOptions
    {
        public string ApplicationDatabaseName { get; set; }
        public string ConnectionString { get; set; }
        public string MongodExePath { get; set; }
        public string MongoFilesDirectory { get; set; }
    }
}