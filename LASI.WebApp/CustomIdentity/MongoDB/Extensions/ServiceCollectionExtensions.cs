using System;
using System.Linq;
using LASI.WebApp.Models;
using Microsoft.Framework.DependencyInjection;

namespace LASI.WebApp.CustomIdentity.MongoDB.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDB(this IServiceCollection services, Func<MongoDBConfiguration> mongoConfigFactory) => services
                .AddSingleton(provider => mongoConfigFactory())
                .AddSingleton<MongoDBService>()
                .AddSingleton<IRoleProvider<UserRole>>(provider => new MongoDBRoleProvider(provider.GetService<MongoDBService>()))
                .AddSingleton<IUserProvider<ApplicationUser>>(provider => new MongoDBUserProvider(provider.GetService<MongoDBService>()))
                .AddSingleton<IDocumentProvider<UserDocument>>(provider => new MongoDBDocumentProvider(provider.GetService<MongoDBService>()));
        public static IServiceCollection AddMongoDB(this IServiceCollection services, Action<MongoDBOptions> configureOptions)
        {
            var options = new MongoDBOptions();
            configureOptions(options);
            return services.AddMongoDB(() => new MongoDBConfiguration(options));
        }
        public static IServiceCollection AddMongoDB(this IServiceCollection services, MongoDBConfiguration config) => AddMongoDB(services, () => config);
    }
}