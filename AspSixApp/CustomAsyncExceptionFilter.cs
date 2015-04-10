using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace AspSixApp
{
    /// <summary>
    /// This is just an experimental exception filter to explore the concept. As the front end moves toward a single page application,
    /// Web Api style constructs such as these will probably be usefull.
    /// </summary>
    public class CustomAsyncExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var message = $"{context.ActionDescriptor.Name}\n failed with: {context.Exception.GetType()}\n with message: {context.Exception.Message}";
            //context.HttpContext.Response.Body = new System.IO.MemoryStream();
            //context.HttpContext.Response.StatusCode = 400;
            context.HttpContext.Response.ContentType = "Application/JSON";
            await context.HttpContext.Response.WriteAsync(message.Aggregate(string.Empty, (f, y) => f + y));
        }
    }
}