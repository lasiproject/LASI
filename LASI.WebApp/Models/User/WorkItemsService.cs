using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities;

namespace LASI.WebApp.Models.User
{
    using static Enumerable;
    public class UserWorkItemsService : IWorkItemsService
    {
        public IEnumerable<WorkItem> GetAllWorkItemsForUser(string userId) => workItems.GetValueOrDefault(userId, Empty<WorkItem>());

        public WorkItem GetWorkItemForUserDocument(string userId, string documentId) => workItems.GetValueOrDefault(userId, Empty<WorkItem>()).FirstOrDefault(w => w.Id == documentId);
        public void UpdateWorkItemForUser(string userId, WorkItem item)
        {
            workItems.AddOrUpdate(key: userId, addValue: new[] { item }, updateValueFactory: (id, items) =>
            {
                items = items.DistinctBy(t => t.Id);
                var target = items.SingleOrDefault(w => w.Id == item.Id);
                if (target != null)
                {
                    target.Name = item.Name;
                    target.PercentComplete = item.PercentComplete;
                    target.State = item.State;
                    target.StatusMessage = item.StatusMessage;
                }
                else
                {
                    items = items.Append(item);
                }
                return items;
            });
        }
        private readonly ConcurrentDictionary<string, IEnumerable<WorkItem>> workItems = new ConcurrentDictionary<string, IEnumerable<WorkItem>>();
    }
}
