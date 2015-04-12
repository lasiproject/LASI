using System;
using System.Linq;
using AspSixApp.Models;
using Microsoft.Framework.DependencyInjection;

namespace AspSixApp.CustomIdentity.MongoDB.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDB(this IServiceCollection services, Func<MongoDBConfiguration> mongoConfigFactory) => services
                .AddSingleton(provider => mongoConfigFactory)
                .AddSingleton<MongoDBService>()
                .AddSingleton<IRoleProvider<UserRole>>(provider => new MongoDBRoleProvider(provider.GetService<MongoDBService>()))
                .AddSingleton<IUserProvider<ApplicationUser>>(provider => new MongoDBUserProvider(provider.GetService<MongoDBService>()))
                .AddSingleton<IInputDocumentProvider<UserDocument>>(provider => new MongoDBUserDocumentProvider(provider.GetService<MongoDBService>()));

        public static IServiceCollection AddMongoDB(this IServiceCollection services, MongoDBConfiguration mongoConfig) => AddMongoDB(services, () => mongoConfig);
    }
}