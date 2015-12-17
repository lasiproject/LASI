using Microsoft.AspNet.Mvc;

namespace LASI.WebApp.Http
{
    internal class HttpForbiddenResult : JsonResult
    {
        public HttpForbiddenResult(string message) : base(new { StatusCode = 403, Message = message }) { }
    }
}