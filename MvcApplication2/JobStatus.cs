using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.WebService
{
    /// <summary>
    /// Represents a unit of processing delegated to an asynchronous agent.
    /// </summary>
    public class JobStatus
    {


        public JobStatus(int jobId, string currentOperation, double percentComplete) { JobId = jobId; CurrentOperation = currentOperation; PercentComplete = percentComplete; }
        public int JobId { get; private set; }
        /// <summary>
        /// Gets a textual description of the ongoing work. 
        /// </summary>
        public string CurrentOperation { get; private set; }

        /// <summary>
        /// Gets the percentage complete of the work corresponding to the Job.
        /// </summary>
        public double PercentComplete { get; private set; }

        /// <summary>
        /// Returns the Job serialized as a JSON string.
        /// </summary>
        /// <returns>The Job serialized as a JSON string.</returns>
        public string ToJson() {
            var settings = new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver { }
            };
            return JsonConvert.SerializeObject(this, settings);
        }
        public override bool Equals(object obj) {
            return obj != null && this == (JobStatus)obj;
        }
        public override int GetHashCode() {
            return JobId.GetHashCode() ^ CurrentOperation.GetHashCode() ^ PercentComplete.GetHashCode();
        }
        public static bool operator ==(JobStatus left, JobStatus right) {
            return
                left.JobId == right.JobId &&
                left.CurrentOperation == right.CurrentOperation &&
                left.PercentComplete == right.PercentComplete;
        }
        public static bool operator !=(JobStatus left, JobStatus right) {
            return !(left == right);
        }
    }
}
