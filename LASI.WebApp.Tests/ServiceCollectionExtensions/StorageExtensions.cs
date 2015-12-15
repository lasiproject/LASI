﻿using System;
using System.IO;
using System.Linq;
using LASI.Utilities;
using LASI.WebApp.Persistence;
using LASI.WebApp.Models;
using LASI.WebApp.Models.User;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace LASI.WebApp.Tests.ServiceCollectionExtensions
{
    public static class StorageExtensions
    {
        public static IServiceCollection AddInMemoryStores(this IServiceCollection services, ApplicationUser user)
        {
            services.AddSingleton(provider =>
            {
                return new MockHostingEnvironment();
            });
            services.AddSingleton<IUserAccessor<ApplicationUser>>(provider => new InMemoryUserProvider());
            services.AddSingleton<IRoleAccessor<UserRole>>(provider => new InMemoryRoleProvider());
            services.TryAdd(new ServiceDescriptor(typeof(IDocumentAccessor<UserDocument>), CreateMockDocumentProvider(user)));
            return services;
        }
        private static IDocumentAccessor<UserDocument> CreateMockDocumentProvider(ApplicationUser user)
        {
            var documents = new[] {
                new UserDocument {
                    _id = MongoDB.Bson.ObjectId.GenerateNewId(),
                    DateUploaded = DateTimeOffset.Now.ToString(),
                    Name = "Test Document One",
                    UserId = user.Id,
                    Content = MockDocumentContent1
                },
                new UserDocument {
                    _id = MongoDB.Bson.ObjectId.GenerateNewId(),
                    DateUploaded = DateTimeOffset.Now.ToString(),
                    Name = "Test Document Two",
                    UserId = user.Id,
                    Content = MockDocumentContent2
                },
                new UserDocument {
                    _id = MongoDB.Bson.ObjectId.GenerateNewId(),
                    DateUploaded = DateTimeOffset.Now.ToString(),
                    Name = "Test Document Three",
                    UserId = user.Id,
                    Content = MockDocumentContent3
                }
            };
            user.Documents = documents;
            return new MockDocumentAccessor(documents);
        }
        public class MockHostingEnvironment : IHostingEnvironment
        {
            public string EnvironmentName { get; set; } = "Development";
            public IFileProvider WebRootFileProvider { get { return new PhysicalFileProvider(WebRootPath); } set { throw new NotSupportedException(); } }
            public string WebRootPath { get; set; } = Directory.GetCurrentDirectory();
        }
        public static IServiceCollection AddMockWorkItemsService(this IServiceCollection services, ApplicationUser user)
        {
            services.TryAdd(new ServiceDescriptor(typeof(IDocumentAccessor<UserDocument>), CreateMockDocumentProvider(user)));
            return services.AddSingleton(provider =>
            {
                var documentProvider = provider.GetService<IDocumentAccessor<UserDocument>>();
                return new MockWorkItemsService(documentProvider);
            });
        }

        private class MockDocumentAccessor : IDocumentAccessor<UserDocument>
        {
            public MockDocumentAccessor(IEnumerable<UserDocument> documents)
            {
                this.documents = documents.GroupBy(d => d.UserId).ToDictionary(g => g.Key, g => g.AsEnumerable());
            }
            public string AddForUser(UserDocument document) => AddForUser(document.UserId, document);

            public string AddForUser(string userId, UserDocument document)
            {
                if (documents.ContainsKey(userId))
                {
                    documents[userId] = documents[userId].Append(document);
                }
                return document.Id;
            }

            public IEnumerable<UserDocument> GetAllForUser(string userId) => documents.GetValueOrDefault(userId, Enumerable.Empty<UserDocument>());

            public UserDocument GetById(string userId, string documentId)
            {
                return documents.GetValueOrDefault(userId).First(d => d.Id == documentId && d.UserId == userId);
            }

            public void RemoveById(string userId, string documentId)
            {
                if (documents.ContainsKey(userId))
                {
                    documents[userId] = documents[userId].Where(d => d.Id != documentId.ToString() && d.UserId != userId);
                }
            }
            private Dictionary<string, IEnumerable<UserDocument>> documents;

        }


        private class MockWorkItemsService : IWorkItemsService
        {

            public MockWorkItemsService(IDocumentAccessor<UserDocument> documentProvider)
            {
                this.documentProvider = documentProvider;
            }
            public IEnumerable<WorkItem> GetAllWorkItemsForUser(string userId)
            {

                var userWorkItemIdGenerator = 0;
                return from document in documentProvider.GetAllForUser(userId)
                       select new WorkItem
                       {
                           Id = (++userWorkItemIdGenerator).ToString(),
                           Name = $"{document.Name}WorkItem",
                           PercentComplete = 0,
                           State = TaskState.Pending,
                           StatusMessage = TaskState.Pending.ToString()
                       };
            }
            public WorkItem GetWorkItemForUserDocument(string userId, string documentId)
            {
                throw new NotImplementedException();
            }

            public void UpdateWorkItemForUser(string userId, WorkItem item)
            {
                throw new NotImplementedException();
            }
            private readonly IDocumentAccessor<UserDocument> documentProvider;
        }

        private const string MockDocumentContent1 =
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
        private const string MockDocumentContent2 =
            @"JSON is a text format that facilitates structured data interchange between all programming languages. JSON
            is syntax of braces, brackets, colons, and commas that is useful in many contexts, profiles, and applications.
            JSON was inspired by the object literals of JavaScript aka ECMAScript as defined in the ECMAScript
            Language Specification, third Edition. It does not attempt to impose ECMAScript’s internal data
            representations on other programming languages. Instead, it shares a small subset of ECMAScript’s textual
            representations with all other programming languages.
            JSON is agnostic about numbers. In any programming language, there can be a variety of number types of
            various capacities and complements, fixed or floating, binary or decimal. That can make interchange between
            different programming languages difficult. JSON instead offers only the representation of numbers that
            humans use: a sequence of digits. All programming languages know how to make sense of digit sequences
            even if they disagree on internal representations. That is enough to allow interchange.
            JSON text is a sequence of Unicode code points.
            Programming languages vary widely on whether they support objects, and if so, what characteristics and
            constraints the objects offer. The models of object systems can be wildly divergent and are continuing to
            evolve. JSON instead provides a simple notation for expressing collections of name/value pairs. Most
            programming languages will have some feature for representing such collections, which can go by names like
            record, struct, dict, map, hash, or object.
            JSON also provides support for ordered lists of values. All programming languages will have some feature for
            representing such lists, which can go by names like array, vector, or list. Because objects and arrays
            can nest, trees and other complex data structures can be represented. By accepting JSON’s simple
            convention, complex data structures can be easily interchanged between incompatible programming
            languages.
            JSON does not support cyclic graphs, at least not directly. JSON is not indicated for applications requiring
            binary data.
            It is expected that other standards will refer to this one, strictly adhering to the JSON text format, while
            imposing restrictions on various encoding details. Such standards may require specific behaviours. JSON
            itself specifies no behaviour.
            Because it is so simple, it is not expected that the JSON grammar will ever change. This gives JSON, as a
            foundational notation, tremendous stability. JSON was first presented to the world at the JSON.org website in
            2001. JSON stands for JavaScript Object Notation.";

        private const string MockDocumentContent3 =
            @"Perhaps the male hero archetype glorified by women and society in general is not so much a manifestation of sexism.
            Perhaps it comes from a self-ascribed or perceived inability to empathize with the opposite sex which allows for abstract idealized
            traits to then be plausibly ascribed to its members.";
    }
}
