using System;
using System.IO;
using System.Linq;
using LASI.Utilities;
using LASI.WebApp.CustomIdentity;
using LASI.WebApp.Models;
using LASI.WebApp.Models.User;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection;
using Moq;

namespace LASI.WebApp.Tests.ServiceCollectionExtensions
{
    public static class StorageExtensions
    {
        public static IServiceCollection AddInMemoryStores(this IServiceCollection services, ApplicationUser user)
        {
            services.AddSingleton(provider =>
            {
                var mock = new Mock<IHostingEnvironment>();
                mock.SetupGet(m => m.EnvironmentName)
                    .Returns("Development");
                mock.SetupGet(m => m.WebRootPath)
                    .Returns(Directory.GetCurrentDirectory());
                mock.SetupGet(m => m.WebRootFileProvider)
                    .Returns(new PhysicalFileProvider(mock.Object.WebRootPath));
                return mock.Object;
            })
            .AddSingleton<IUserProvider<ApplicationUser>>(provider => new InMemoryUserProvider())
            .AddSingleton<IRoleProvider<UserRole>>(provider => new InMemoryRoleProvider());
            services.TryAdd(new ServiceDescriptor(typeof(IDocumentProvider<UserDocument>), CreateMockDocumentProvider(user)));
            return services;
        }
        private static IDocumentProvider<UserDocument> CreateMockDocumentProvider(ApplicationUser user)
        {
            var document = new UserDocument
            {
                DateUploaded = DateTime.Now.ToString(),
                Name = "Test Document",
                UserId = user.Id,
                _id = MongoDB.Bson.ObjectId.GenerateNewId(),
                Content = MockDocumentContent
            };
            var mock = new Mock<IDocumentProvider<UserDocument>>();
            mock.Setup(m => m.GetAllForUser(user.Id))
                .Returns(new[] { document });
            mock.Setup(m => m.RemoveByIds(user.Id, document._id.ToString()))
                .Callback(() => user.Documents = user.Documents.Where(d => d._id != document._id));
            mock.Setup(m => m.AddForUser(user.Id, document))
                .Returns(document._id.ToString())
                .Callback(() => user.Documents = user.Documents.Append(document));
            mock.Setup(m => m.GetByIds(user.Id, document._id.ToString()))
                .Returns(document);
            return mock.Object;
        }

        public static IServiceCollection AddMockWorkItemsService(this IServiceCollection services, ApplicationUser user)
        {
            services.TryAdd(new ServiceDescriptor(typeof(IDocumentProvider<UserDocument>), CreateMockDocumentProvider(user)));
            return services.AddSingleton<IWorkItemsService>(provider =>
            {
                var documentProvider = provider.GetService<IDocumentProvider<UserDocument>>();
                var userWorkItemIdGenerator = 0;
                var mock = new Mock<IWorkItemsService>();
                mock.Setup(m => m.GetAllWorkItemsForUser(user.Id))
                    .Returns(from document in documentProvider.GetAllForUser(user.Id)
                             select new WorkItem
                             {
                                 Id = userWorkItemIdGenerator++.ToString(),
                                 Name = $"{document.Name}WorkItem",
                                 PercentComplete = 0,
                                 State = TaskState.Pending,
                                 StatusMessage = TaskState.Pending.ToString()
                             });
                return mock.Object;
            });
        }
        private const string MockDocumentContent =
            @"Studying is the main source of knowledge. Books are indeed never failing friends
            of man. For a mature mind, reading is the greatest source of pleasure and solace
            to distressed minds. The study of good books ennobles us and broadens our outlook.
            Therefore, the habit of reading should be cultivated. A student should never confine
            himself to his schoolbooks only. He should not miss the pleasure locked in the classics,
            poetry, drama, history, philosophy etc. We can derive benefit from other’s experiences 
            with the help of books. The various sufferings, endurance and joy described in books 
            enable us to have a closer look at human life. They also inspire us to face the 
            hardships of life courageously. Nowadays there are innumerable books and time is scarce. 
            So we should read only the best and the greatest among them. With the help of books we 
            shall be able to make our thinking mature and our life more meaningful and worthwhile.";
    }
}
