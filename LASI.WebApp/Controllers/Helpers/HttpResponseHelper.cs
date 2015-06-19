using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LASI.WebApp.Controllers.Helpers
{
    public static class HttpResponseHelper
    {
        public const int UnprocessableEntity = 422;

        public static async Task<T> WriteNotFoundResponseAsnyc<T>(this HttpContext context, string message) where T : class
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(message);
            return await Task.FromResult<T>(null);
        }

    }
}
