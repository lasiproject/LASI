using LASI.Algorithm.DocumentConstructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Analysis
{
    public class ProcessingTask
    {
        public ProcessingTask(Document document, Task workToPerform, string initializationMessage, string completionMessage, double percentWorkRepresented) {
            Document = document;
            WorkToPerform = workToPerform;
            InitializationMessage = initializationMessage;
            CompletionMessage = completionMessage;
            PercentWorkRepresented = percentWorkRepresented;

        }
        public Document Document {
            get;
            private set;
        }
        public Task WorkToPerform {
            get;
            private set;
        }

        public string InitializationMessage {
            get;
            private set;
        }

        public string CompletionMessage {
            get;
            private set;
        }

        public double PercentWorkRepresented {
            get;
            private set;
        }
    }
    public class ProcessingTask<T>
    {
        public ProcessingTask(Document document, Task<T> workToPerform, string initializationMessage, string completionMessage, double percentWorkRepresented) {
            Document = document;
            WorkToPerform = workToPerform;
            InitializationMessage = initializationMessage;
            CompletionMessage = completionMessage;
            PercentWorkRepresented = percentWorkRepresented;

        }
        public Document Document {
            get;
            private set;
        }
        public Task<T> WorkToPerform {
            get;
            private set;
        }

        public string InitializationMessage {
            get;
            private set;
        }

        public string CompletionMessage {
            get;
            private set;
        }

        public double PercentWorkRepresented {
            get;
            private set;
        }
    }
}
