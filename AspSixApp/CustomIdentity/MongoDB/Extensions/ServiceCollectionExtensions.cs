using System;
using System.Linq;
using AspSixApp.Models;
using Microsoft.Framework.DependencyInjection;

namespace AspSixApp.CustomIdentity.MongoDB.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, Func<IServiceProvider, MongoDBConfiguration> mongoConfigFactory)
        {
            return services
                .AddSingleton(mongoConfigFactory)
                .AddSingleton<MongoDBService>()
                .AddSingleton<IInputDocumentStore<UserDocument>>(provider => new MongoDBUserDocumentStore(provider.GetService<MongoDBService>()))
                .AddSingleton<IRoleProvider<UserRole>>(provider => new MongoDBRoleProvider(provider.GetService<MongoDBService>()))
                .AddSingleton<IUserProvider<ApplicationUser>>(provider => new MongoDBUserProvider(provider.GetService<MongoDBService>()));
        }
    }
}