using LASI.Algorithm.DocumentConstructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Analysis
{
    /// <summary>
    /// Associates a Task, the Document to which it applies, and initialization and completion feedback properties.
    /// </summary>
    public class ProcessingTask
    {
        /// <summary>
        /// Initializes a new Instance of the Processing Task class with the given Document, Task to perform, initialization message, completion message, and percentage of total work represented. 
        /// </summary>
        /// <param name="document">The Document on which the ProcessingTask's work will be performed.</param>
        /// <param name="workToPerform">A Task object repsenting an operation over the given document.</param>
        /// <param name="initializationMessage">A message indicating the start of specific the ProcessingTask.</param>
        /// <param name="completionMessage">A message indicating the end of specific the ProcessingTask.</param>
        /// <param name="percentWorkRepresented">An arbitrary double value corresponding to a relative amount of work the ProcessingTask represents.</param>
        public ProcessingTask(Document document, Task workToPerform, string initializationMessage, string completionMessage, double percentWorkRepresented) {
            Document = document;
            Task = workToPerform;
            InitializationMessage = initializationMessage;
            CompletionMessage = completionMessage;
            PercentWorkRepresented = percentWorkRepresented;

        }
        /// <summary>
        /// Gets the document over which the ProcessingTask will operate.
        /// </summary>
        public Document Document {
            get;
            private set;
        }
        /// <summary>
        /// Gets the work the ProcessingTask will perform.
        /// </summary>
        public Task Task {
            get;
            private set;
        }
        /// <summary>
        /// Gets a message indicating the start of specific the ProcessingTask.
        /// </summary>
        public string InitializationMessage {
            get;
            private set;
        }
        /// <summary>
        /// Gets a message indicating the end of specific the ProcessingTask.
        /// </summary>
        public string CompletionMessage {
            get;
            private set;
        }
        /// <summary>
        /// Gets an arbitrary double value corresponding to a relative amount of work the ProcessingTask represents.
        /// </summary>
        public double PercentWorkRepresented {
            get;
            private set;
        }
    }
    /// <summary>
    /// Associates a result returning Task, the Document to which it applies, and initialization and completion feedback properties.
    /// </summary>
    public class ProcessingTask<T>
    {
        /// <summary>
        /// Initializes a new Instance of the Processing Task class with the given Document, Task of T to perform, initialization message, completion message, and percentage of total work represented. 
        /// </summary>
        /// <param name="document">The Document on which the ProcessingTask's work will be performed.</param>
        /// <param name="workToPerform">A Task of T object repsenting an operation over the given document.</param>
        /// <param name="initializationMessage">A message indicating the start of specific the ProcessingTask.</param>
        /// <param name="completionMessage">A message indicating the end of specific the ProcessingTask.</param>
        /// <param name="percentWorkRepresented">An arbitrary double value corresponding to a relative amount of work the ProcessingTask represents.</param>
        public ProcessingTask(Document document, Task<T> workToPerform, string initializationMessage, string completionMessage, double percentWorkRepresented) {
            Document = document;
            Task = workToPerform;
            InitializationMessage = initializationMessage;
            CompletionMessage = completionMessage;
            PercentWorkRepresented = percentWorkRepresented;

        }
        /// <summary>
        /// Gets the document over which the ProcessingTask will operate.
        /// </summary>
        public Document Document {
            get;
            private set;
        }
        /// <summary>
        /// Gets the work the ProcessingTask will perform, resulting in an instance of T.
        /// </summary>
        public Task<T> Task {
            get;
            private set;
        }
        /// <summary>
        /// Gets a message indicating the start of specific the ProcessingTask.
        /// </summary>
        public string InitializationMessage {
            get;
            private set;
        }
        /// <summary>
        /// Gets a message indicating the end of specific the ProcessingTask.
        /// </summary>
        public string CompletionMessage {
            get;
            private set;
        }
        /// <summary>
        /// Gets an arbitrary double value corresponding to a relative amount of work the ProcessingTask represents.
        /// </summary>
        public double PercentWorkRepresented {
            get;
            private set;
        }
    }
}
