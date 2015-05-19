using System;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace LASI.WebApp.CustomIdentity.MongoDB.Extensions
{
    public static class MongoQueryExtensions
    {
        public static IMongoQuery And(this IMongoQuery query, IMongoQuery other) => Query.And(other);

    }
}