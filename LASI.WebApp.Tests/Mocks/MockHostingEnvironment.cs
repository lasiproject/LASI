using System;
using System.IO;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;

namespace LASI.WebApp.Tests.Mocks
{
    public class MockHostingEnvironment : IHostingEnvironment
    {
        public string EnvironmentName { get; set; } = "Development";
        public IFileProvider WebRootFileProvider { get { return new PhysicalFileProvider(WebRootPath); } set { throw new NotSupportedException(); } }
        public string WebRootPath { get; set; } = Directory.GetCurrentDirectory();
    }
}
