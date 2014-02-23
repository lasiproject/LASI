using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.WebService.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LASI.WebService;


namespace LASI.WebService.Controllers.Tests
{
    [TestClass()]
    public class JobTests
    {
        [TestMethod()]
        public void JobTest() {
            var job = new Job("parsing", 10);
            Assert.AreEqual(job.PercentComplete, 10d);
            Assert.AreEqual(job.CurrentOperation, "parsing");
        }

        [TestMethod()]
        public void ToJsonTest() {
            var job = new Job("parsing", 10.0);
            Assert.AreEqual(string.Format("{{\"JobId\":\"{0}\",\"CurrentOperation\":\"{1}\",\"PercentComplete\":{2:0.0}}}",
                job.JobId,
                job.CurrentOperation,
                job.PercentComplete),
            job.ToJson());
        }
    }
}
