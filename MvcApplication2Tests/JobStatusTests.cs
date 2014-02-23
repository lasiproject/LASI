using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.WebService.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LASI.WebService;
using System.Reflection;


namespace LASI.WebService.Controllers.Tests
{
    [TestClass()]
    public class JobStatusTests
    {
        [TestMethod()]
        public void JobTest() {
            var id = new Random().Next();
            var job = new JobStatus(id, "parsing", 10);
            Assert.AreEqual(job.PercentComplete, 10d);
            Assert.AreEqual(job.CurrentOperation, "parsing");
        }

        [TestMethod()]
        public void ToJsonTest() {
            var id = new Random().Next();

            var job = new JobStatus(id, "parsing", 10.0);
            var json = job.ToJson();
            Assert.AreEqual(string.Format("{{\"{0}\":{1},\"{2}\":\"{3}\",\"{4}\":{5:0.0}}}",
               job.GetType().GetProperty("JobId").Name, job.JobId,
            job.GetType().GetProperty("CurrentOperation").Name, job.CurrentOperation,
               job.GetType().GetProperty("PercentComplete").Name, job.PercentComplete),
               json, ignoreCase: true);
            var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<JobStatus>(json);
            Assert.AreEqual(job, deserialized);
        }
        [TestMethod()]
        public void EqualsTest() {
            var id = new Random().Next(0, 4096);
            var job1 = new JobStatus(id, "parsing", 10.0);
            var job2 = new JobStatus(id, "parsing", 10.0);
            Assert.AreEqual(job1, job2);
            var job3 = new JobStatus(4097, "parsing", 10.0);
            Assert.AreNotEqual(job1, job3);
        }

        [TestMethod()]
        public void GetHashCodeTest() {
            var id = new Random().Next(0, 4096);
            var job1 = new JobStatus(id, "parsing", 10.0);
            var job2 = new JobStatus(id, "parsing", 10.0);
            Assert.AreEqual(job1.GetHashCode(), job2.GetHashCode());
            var job3 = new JobStatus(4097, "parsing", 10.0);
            Assert.AreNotEqual(job1.GetHashCode(), job3.GetHashCode());
        }
        [TestMethod()]
        public void Op_EqualityTest() {
            var id = new Random().Next(0, 4096);
            var job1 = new JobStatus(id, "parsing", 10.0);
            var job2 = new JobStatus(id, "parsing", 10.0);
            Assert.IsTrue(job1 == job2);
            Assert.IsFalse(job1 != job2);
            var job3 = new JobStatus(4097, "parsing", 10.0);
            Assert.IsFalse(job1 == job3);
            Assert.IsTrue(job1 != job3);
        }
        [TestMethod()]
        public void Op_InequalityTest() {
            var id = new Random().Next(0, 4096);
            var job1 = new JobStatus(id, "parsing", 10.0);
            var job2 = new JobStatus(id, "parsing", 10.0);
            Assert.IsTrue(job1 == job2);
            Assert.IsFalse(job1 != job2);
            var job3 = new JobStatus(4097, "parsing", 10.0);
            Assert.IsFalse(job1 == job3);
            Assert.IsTrue(job1 != job3);
        }
    }
}
