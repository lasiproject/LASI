using System;
using System.Linq;
using LASI.WebApp.Models;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.OptionsModel;

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
        public static IServiceCollection AddMongoDB(this IServiceCollection services, Action<MongoDBOptions> setupAction) => services
            .AddSingleton<MongoDBOptions>().Configure(setupAction)
            .AddMongoDB(() => new MongoDBConfiguration((MongoDBOptions)services.Where(s => s.Lifetime == ServiceLifetime.Singleton).First(s => s.ServiceType == typeof(MongoDBOptions)).ImplementationInstance));
        public static IServiceCollection AddMongoDB(this IServiceCollection services, MongoDBConfiguration config) => AddMongoDB(services, () => config);
    }
}