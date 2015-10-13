using System;
using System.Runtime.Serialization;

namespace LASI.WebApp.Exceptions
{
    [Serializable]
    public class HttpResponseException : Exception
    {
        public HttpResponseException(int statusCode)
        {
            if (statusCode < 400)
            {
                throw new InvalidOperationException($"HttpResponseException instances must represent HTTP error response codes. Codes below 400 are not permitted");
            }
            this.StatusCode = statusCode;
        }
        protected HttpResponseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
            StatusCode = info.GetInt32(nameof(StatusCode));
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(StatusCode), StatusCode);
        }
        public int StatusCode { get; }
    }
}
