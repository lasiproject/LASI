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
    public struct JobStatus
    {
        public JobStatus(int jobId, string message, double percent) {
            JobId = jobId;
            Message = message;
            Percent = percent;
        }
        public int JobId { get; }
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
        public string ToJson() => JsonConvert.SerializeObject(this);


        public override bool Equals(object obj) => obj is JobStatus && this == (JobStatus)obj;

        public override int GetHashCode() => JobId.GetHashCode() ^ Message.GetHashCode() ^ Percent.GetHashCode();

        public static bool operator ==(JobStatus left, JobStatus right) {
            return left.JobId == right.JobId && left.Message == right.Message && left.Percent == right.Percent;
        }
        public static bool operator !=(JobStatus left, JobStatus right) {
            return !(left == right);
        }
    }
}
