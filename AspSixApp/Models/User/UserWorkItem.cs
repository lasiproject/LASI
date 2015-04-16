using System;
using AspSixApp.Controllers;

namespace AspSixApp.Models.User
{
    public class UserWorkItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public TaskState State { get; set; }
        public double PercentComplete { get; set; }
        public string StatusMessage { get; set; }
    }
}