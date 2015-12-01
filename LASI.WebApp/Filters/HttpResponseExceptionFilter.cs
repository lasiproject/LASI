using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;

namespace LASI.WebApp.Filters
{
    /// <summary>
    /// This is just an experimental exception filter to explore the concept. As the front end moves toward a single page application,
    /// Web Api style constructs such as these will probably be useful.
    /// </summary>
    public class HttpResponseExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            //var httpResponseException = context.Exception as HttpResponseException;
            //if (httpResponseException != null)
            //{
            //    await Task.FromException(context.Exception);
            //}
            switch (context.HttpContext.Response.StatusCode)
            {
                case 401:
                context.Result = new JsonResult(new HttpUnauthorizedResult());
                break;
            }
            await Task.CompletedTask;
            //#if DEBUG
            //            var message = $"{context.ActionDescriptor.Name}\n failed with: {httpResponseException.GetType()}\n with message: {httpResponseException.Message}\n with status code {httpResponseException.StatusCode}";
            //            //context.HttpContext.Response.Body = new System.IO.MemoryStream();

            //            //context.HttpContext.Response.ContentType = "Application/JSON";
            //            await context.HttpContext.Response.WriteAsync(message);
            //#endif
            //context.HttpContext.Response.StatusCode = httpResponseException.StatusCode;
        }
    }

}
