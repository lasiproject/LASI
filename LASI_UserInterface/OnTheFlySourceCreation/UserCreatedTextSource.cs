using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;

namespace LASI.UserInterface.OnTheFlySourceCreation
{
    public sealed class UserCreatedTextSource : IRawTextSource
    {
        public UserCreatedTextSource(string text, string name) {
            DataName = name;
            content = text;
        }
        public UserCreatedTextSource(IEnumerable<string> lines, string name) {
            content = String.Join("\n", lines);
            DataName = name;
        }

        private string content;

        public string GetText() {
            return content;
        }

        public async Task<string> GetTextAsync() {
            return await Task.Run(() => content).ContinueWith(t => t.Result, TaskScheduler.FromCurrentSynchronizationContext());
        }


        public string DataName {
            get;
            private set;
        }
    }
}
