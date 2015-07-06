using LASI.WebApp.Models;
using LASI.WebApp.Tests.TestAttributes;
using MongoDB.Bson;

namespace LASI.WebApp.Tests.ControllerTests
{
    [PreconfigureLASI]
    public class ControllerTestsBase
    {
        private const string UserName = "testuser@test.com";
        private static readonly ObjectId userId = ObjectId.GenerateNewId();
        private static readonly ApplicationUser user = new ApplicationUser
        {
            _id = userId,
            UserName = UserName,
            Email = UserName
        };

        public static ApplicationUser User => user;
    }
}