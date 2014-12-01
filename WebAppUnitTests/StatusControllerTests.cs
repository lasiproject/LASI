using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace LASI.WebApp.Controllers.Tests
{
    [TestClass]
    public class StatusControllerTests
    {
        [TestMethod]
        public void GetJobStatusTest1() {
            var target = new JobController();
            var job = new JobStatus(null, 0);
            target.Post(job);
            dynamic json = target.Get(0);
            Assert.IsTrue(json.percent == 0);
            Assert.IsTrue(json.message == null);
        }
    }
}
