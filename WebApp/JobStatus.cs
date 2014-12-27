using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.WebApp
{
    /// <summary>
    /// Represents a unit of processing delegated to an asynchronous agent.
    /// </summary>
    public class JobStatus
    {
        public JobStatus(string message, double percent, int id = 0) {
            Id = id;
            Message = message;
            Percent = percent;
        }
        public int Id { get; internal set; }
        /// <summary>
        /// Gets a textual description of the ongoing work. 
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the percentage complete of the work corresponding to the Job.
        /// </summary>
        public double Percent { get; }

        /// <summary>
        /// Returns the Job serialized as a JSON string.
        /// </summary>
        /// <returns>The Job serialized as a JSON string.</returns>
        public string ToJson() => JsonConvert.SerializeObject(this, serializerSettings);


        public override bool Equals(object obj) => obj is JobStatus && this == (JobStatus)obj;

        public override int GetHashCode() => Id.GetHashCode() ^ Message.GetHashCode() ^ Percent.GetHashCode();

        public static bool operator ==(JobStatus left, JobStatus right) {
            return left.Id == right.Id && left.Message == right.Message && left.Percent == right.Percent;
        }
        public static bool operator !=(JobStatus left, JobStatus right) {
            return !(left == right);
        }
        private static readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            MissingMemberHandling = MissingMemberHandling.Error
        };
    }
}
