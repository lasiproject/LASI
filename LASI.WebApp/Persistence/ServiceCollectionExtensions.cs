using LASI.WebApp.Models;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace LASI.WebApp.Persistence.MongoDB.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddInMemoryDatabase(this IServiceCollection services) =>

            services.AddSingleton<IRoleAccessor<UserRole>>(provider => new InMemoryRoleProvider())
                    .AddSingleton<IUserAccessor<ApplicationUser>>(provider => new InMemoryUserProvider())
                    .AddSingleton<IDocumentAccessor<UserDocument>>(provider => new InMemoryDocumentProvider());

        public static IServiceCollection AddMongoDB(this IServiceCollection services, MongoDBConfiguration mongoConfig)
        {
            services.AddSingleton(p => new MongoDBService(mongoConfig));
            AddCommonServices(services);
            return services;
        }

        public static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration configuration) =>
            services.AddMongoDB(new MongoDBConfiguration(new MongoDBOptions
            {
                ApplicationBasePath = configuration.GetMongoDBSetting(nameof(MongoDBOptions.ApplicationBasePath)),
                CreateProcess = bool.Parse(configuration.GetMongoDBSetting(nameof(MongoDBOptions.CreateProcess))),
                ApplicationDatabaseName = configuration.GetMongoDBSetting(nameof(MongoDBOptions.ApplicationDatabaseName)),
                DataDbPath = AppContext.BaseDirectory + configuration.GetMongoDBSetting(nameof(MongoDBOptions.DataDbPath)),
                InstanceUrl = configuration.GetMongoDBSetting(nameof(MongoDBOptions.InstanceUrl)),
                MongodExePath = configuration.GetMongoDBSetting(nameof(MongoDBOptions.MongodExePath)),
                OrganizationCollectionName = configuration.GetMongoDBSetting(nameof(MongoDBOptions.OrganizationCollectionName)),
                UserCollectionName = configuration.GetMongoDBSetting(nameof(MongoDBOptions.UserCollectionName)),
                UserDocumentCollectionName = configuration.GetMongoDBSetting(nameof(MongoDBOptions.UserDocumentCollectionName)),
                UserRoleCollectionName = configuration.GetMongoDBSetting(nameof(MongoDBOptions.UserRoleCollectionName))
            }));
        private static string GetMongoDBSetting(this IConfiguration configuration, string key) => configuration[$"MongoDB:{key}"];
        public static IServiceCollection AddMongoDB(this Microsoft.Extensions.DependencyInjection.IServiceCollection services, Action<MongoDBOptions> setupAction)
        {
            var mongoOptions = new MongoDBOptions();
            setupAction(mongoOptions);
            return services
                .AddSingleton(provider => mongoOptions)
                .AddMongoDB(new MongoDBConfiguration(mongoOptions));
        }
        private static void AddCommonServices(IServiceCollection services) => services
               .AddSingleton<IRoleAccessor<UserRole>>(provider => new MongoDBRoleProvider(provider.GetService<MongoDBService>()))
               .AddSingleton<IUserAccessor<ApplicationUser>>(provider => new MongoDBUserProvider(provider.GetService<MongoDBService>()))
               .AddSingleton<IDocumentAccessor<UserDocument>>(provider => new MongoDBDocumentProvider(provider.GetService<MongoDBService>()));
    }
}