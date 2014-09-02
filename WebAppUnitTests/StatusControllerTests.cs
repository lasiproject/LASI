using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.WebApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace LASI.WebApp.Controllers.Tests
{
    [TestClass]
    public class StatusControllerTests
    {
        [TestMethod]
        public void GetJobStatusTest1() {
            HomeController target = new HomeController();
            dynamic json = JsonConvert.DeserializeObject(target.GetJobStatus("1").ToString());
            Assert.IsTrue(json.percent == 0);
            Assert.IsTrue(json.message == null);
        }
    }
}
