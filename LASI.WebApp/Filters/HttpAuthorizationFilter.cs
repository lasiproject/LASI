using System;
using System.Linq;
using System.Threading.Tasks;
using LASI.WebApp.Exceptions;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;

namespace LASI.WebApp.Filters
{

    public class HttpAuthorizationFilterAttribute : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var httpContext = context.HttpContext;
            var user = httpContext.User;

            if (httpContext.Response.StatusCode == 401)

            {

                context.Result = new JsonResult(new HttpUnauthorizedResult());
            }
            await Task.CompletedTask;

        }
    }
}
