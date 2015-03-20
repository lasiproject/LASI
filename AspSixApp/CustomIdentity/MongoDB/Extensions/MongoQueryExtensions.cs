using System;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace AspSixApp.CustomIdentity.MongoDB.Extensions
{
    public static class MongoQueryExtensions
    {
        public static IMongoQuery And(this IMongoQuery query, IMongoQuery other) => Query.And(other);

    }
}