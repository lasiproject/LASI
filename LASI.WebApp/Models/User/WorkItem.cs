namespace LASI.WebApp.Models.User
{
    public class WorkItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public TaskState State { get; set; }
        public double PercentComplete { get; set; }
        public string StatusMessage { get; set; }
    }
}