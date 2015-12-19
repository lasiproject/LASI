using System;
using System.Collections.Generic;
using System.Linq;
using LASI.WebApp.Models;
using LASI.WebApp.Models.User;
using LASI.WebApp.Persistence;

namespace LASI.WebApp.Tests.Mocks
{
    class MockWorkItemsService : IWorkItemsService
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

}