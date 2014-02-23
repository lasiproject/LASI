using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.WebService
{
    public class Job
    {

        public Job(string p1, double p2) {
            CurrentOperation = p1;
            PercentComplete = p2;
            JobId = idGen++;
        }
        
        public int JobId { get; private set; }

        public string CurrentOperation { get; private set; }

        public double PercentComplete { get; private set; }

        public string ToJson() {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
        static int idGen = 0;
    }
}
