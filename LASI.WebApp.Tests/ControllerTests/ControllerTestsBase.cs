using System;
using LASI.WebApp.Models;
using LASI.WebApp.Tests.TestAttributes;
using MongoDB.Bson;

namespace LASI.WebApp.Tests.ControllerTests
{
    [PreconfigureLASI]
    public class ControllerTestsBase
    {
        private const string Email = "testuser@test.com";
        private static readonly ObjectId userId = ObjectId.GenerateNewId();

        private class TestApplicationUser : ApplicationUser
        {
            public override ObjectId _id
            {
                get
                {
                    return userId;
                }
                set { throw new InvalidOperationException(); }
            }
            public override string UserName
            {
                get
                {
                    return ControllerTestsBase.Email;
                }
                set { throw new InvalidOperationException(); }
            }
            public override string Email
            {
                get
                {
                    return ControllerTestsBase.Email;
                }
                set { throw new InvalidOperationException(); }
            }

        }
        private static readonly ApplicationUser user = new TestApplicationUser();

        public static ApplicationUser User => user;
    }
}