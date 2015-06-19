using System;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace LASI.WebApp.Persistence.MongoDB.Extensions
{
    public static class MongoQueryExtensions
    {
        public static IMongoQuery And(this IMongoQuery query, IMongoQuery other) => Query.And(query, other);

    }
}