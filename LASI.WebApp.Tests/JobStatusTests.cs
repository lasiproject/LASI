using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LASI.WebApp.Controllers.Tests
{
    [TestClass]
    public class JobStatusTests
    {
        [TestMethod]
        public void JobTest() {
            var id = new Random().Next();
            var job = new JobStatus("parsing", 10, id);
            Assert.AreEqual(job.Percent, 10d);
            Assert.AreEqual(job.Message, "parsing");
        }

        [TestMethod]
        public void ToJsonTest() {
            var id = new Random().Next();

            var job = new JobStatus("parsing", 10, id);
            var json = job.ToJson();
            Assert.AreEqual(string.Format("{{\"{0}\":{1},\"{2}\":\"{3}\",\"{4}\":{5:0.0}}}", "id", job.Id, "message", job.Message, "percent", job.Percent), json);
            var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<JobStatus>(json, new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            });
            Assert.AreEqual(job, deserialized);
        }
        [TestMethod]
        public void EqualsTest() {
            var id = new Random().Next(0, 4096);
            var job1 = new JobStatus("parsing", 10, id);
            var job2 = new JobStatus("parsing", 10, id);
            Assert.AreEqual(job1, job2);
            var job3 = new JobStatus("parsing", 10.0, 4097);
            Assert.AreNotEqual(job1, job3);
        }

        [TestMethod]
        public void GetHashCodeTest() {
            var id = new Random().Next(0, 4096);
            var job1 = new JobStatus("parsing", 10.0, id);
            var job2 = new JobStatus("parsing", 10.0, id);
            Assert.AreEqual(job1.GetHashCode(), job2.GetHashCode());
            var job3 = new JobStatus("parsing", 10.0, 4097);
            Assert.AreNotEqual(job1.GetHashCode(), job3.GetHashCode());
        }
        [TestMethod]
        public void Op_EqualityTest() {
            var id = new Random().Next(0, 4096);
            var job1 = new JobStatus("parsing", 10.0, id);
            var job2 = new JobStatus("parsing", 10.0, id);
            Assert.IsTrue(job1 == job2);
            Assert.IsFalse(job1 != job2);
            var job3 = new JobStatus("parsing", 10.0, 4097);
            Assert.IsFalse(job1 == job3);
            Assert.IsTrue(job1 != job3);
        }
        [TestMethod]
        public void Op_InequalityTest() {
            var id = new Random().Next(0, 4096);
            var job1 = new JobStatus("parsing", 10.0, id);
            var job2 = new JobStatus("parsing", 10.0, id);
            Assert.IsTrue(job1 == job2);
            Assert.IsFalse(job1 != job2);
            var job3 = new JobStatus("parsing", 10.0, 4097);
            Assert.IsFalse(job1 == job3);
            Assert.IsTrue(job1 != job3);
        }
    }
}
