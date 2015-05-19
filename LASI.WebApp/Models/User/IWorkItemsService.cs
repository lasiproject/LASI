using System.Collections.Generic;

namespace LASI.WebApp.Models.User
{
    public interface IWorkItemsService
    {
        IEnumerable<WorkItem> GetAllWorkItemsForUser(string userId);
        WorkItem GetWorkItemForUserDocument(string userId, string documentId);
        void UpdateWorkItemForUser(string userId, WorkItem item);
    }
}