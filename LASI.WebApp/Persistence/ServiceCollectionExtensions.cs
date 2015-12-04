using System;
using System.Linq;
using LASI.WebApp.Models;
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
            services.AddSingleton(provider => new MongoDBService(mongoConfig));
            AddCommonServices(services);
            return services;
        }

        public static IServiceCollection AddMongoDB(this IServiceCollection services, Action<MongoDBOptions> setupAction)
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