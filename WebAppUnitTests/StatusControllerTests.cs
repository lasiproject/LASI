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
            var target = new HomeController();
            dynamic json = target.GetJobStatus(1.ToString());
            Assert.IsTrue(json.percent == 0);
            Assert.IsTrue(json.message == null);
        }
    }
}
