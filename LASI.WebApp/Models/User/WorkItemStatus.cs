using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LASI.WebApp.Models.Results
{
    /// <summary>
    /// Represents a unit of processing delegated to an asynchronous agent.
    /// </summary>
    public class WorkItemStatus
    {
        public WorkItemStatus(string message, double percent, int id = 0)
        {
            Id = id;
            Message = message;
            Percent = percent;
        }

        public static bool operator !=(WorkItemStatus left, WorkItemStatus right) => !(left == right);

        public static bool operator ==(WorkItemStatus left, WorkItemStatus right) => left.Id == right.Id && left.Message == right.Message && left.Percent == right.Percent;

        public override bool Equals(object obj) => obj is WorkItemStatus && this == (WorkItemStatus)obj;

        public override int GetHashCode() => Id.GetHashCode() ^ Message.GetHashCode() ^ Percent.GetHashCode();

        /// <summary>
        /// Returns the Job serialized as a JSON string.
        /// </summary>
        /// <returns>The Job serialized as a JSON string.</returns>
        public string ToJson() => JsonConvert.SerializeObject(this, serializerSettings);

        /// <summary>
        /// Gets the Id of the current instance.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets a textual description of the ongoing work.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the percentage complete of the work corresponding to the Job.
        /// </summary>
        public double Percent { get; }

        private static readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            MissingMemberHandling = MissingMemberHandling.Error
        };
    }
}