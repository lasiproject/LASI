using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LASI.Utilities;

namespace LASI.WebApp.Models.User
{
    public class DummyUserWorkItemService : IWorkItemsService
    {
        public DummyUserWorkItemService(int itemCount, double maxUpdate, TimeSpan updateInterval)
        {
            InitialItemCount = itemCount;
            this.workItems["x"] = CreateWorkItems(itemCount);
            var rand = CreateRandom(maxUpdate);
            this.timer = new Timer(
                 callback: state =>
                 {
                     foreach (var item in this.workItems["x"])
                     {
                         var updatedCompletionPercent = Math.Min(item.PercentComplete + rand(), 100);
                         switch ((int)Math.Min(Math.Round(item.PercentComplete, MidpointRounding.ToEven), 99))
                         {
                             case 0:
                                 item.State = TaskState.Ongoing;
                                 item.StatusMessage = "In Progress";
                                 break;
                             case 99:
                                 item.State = TaskState.Complete;
                                 item.StatusMessage = "Finished";
                                 break;
                         }
                         item.PercentComplete = updatedCompletionPercent;
                         this.UpdateWorkItemForUser("x", item);
                     }
                 },
                 state: null,
                 dueTime: TimeSpan.FromSeconds(2),
                 period: updateInterval
             );

        }

        private IEnumerable<WorkItem> CreateWorkItems(int itemCount) => from i in Enumerable.Range(0, itemCount)
                                                                        select new WorkItem
                                                                        {
                                                                            Id = $"UserWorkItem{i}",
                                                                            Name = $"Work Item {i}",
                                                                            PercentComplete = 0,
                                                                            State = TaskState.Pending,
                                                                            StatusMessage = "Not yet Started"
                                                                        };

        private static Func<double> CreateRandom(double randMax)
        {
            var random = new Random();
            return () => random.Next((int)randMax);
        }

        public IEnumerable<WorkItem> GetAllWorkItemsForUser(string userId) => workItems.GetValueOrDefault("x", Enumerable.Empty<WorkItem>());

        public WorkItem GetWorkItemForUserDocument(string userId, string documentId) => workItems.GetValueOrDefault("x", Enumerable.Empty<WorkItem>()).FirstOrDefault(w => w.Id == documentId);

        private const string Key = "x"; // x is an arbitrary key

        public void Reset() => this.workItems[Key] = CreateWorkItems(this.InitialItemCount);

        public void UpdateWorkItemForUser(string userId, WorkItem item) => workItems.AddOrUpdate("x", new[] { item }, (key, value) => from i in value select i.Id == item.Id ? item : i);

        private readonly int InitialItemCount;

        private readonly Timer timer;

        private readonly System.Collections.Concurrent.ConcurrentDictionary<string, IEnumerable<WorkItem>> workItems = new System.Collections.Concurrent.ConcurrentDictionary<string, IEnumerable<WorkItem>>();

    }
}