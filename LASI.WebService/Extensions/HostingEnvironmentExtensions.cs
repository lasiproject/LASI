using Microsoft.AspNetCore.Hosting;

namespace LASI.WebService.Extensions
{
    public static class HostingEnvironmentExtensions
    {

        public static void Deconstruct(this IHostingEnvironment env, out bool isDevelopment, out bool isStaging, out bool isProduction)
        {
            isDevelopment = env.IsDevelopment();
            isStaging = env.IsStaging();
            isProduction = env.IsProduction();
        }
    }
}