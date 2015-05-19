using System;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Associates a Task, the Document to which it applies, and initialization and completion
    /// feedback properties.
    /// </summary>
    public class ProcessingTask : IDisposable
    {
        /// <summary>
        /// Initializes a new Instance of the Processing Task class with the given Task,
        /// initialization message, completion message, and percentage of total work represented.
        /// </summary>
        /// <param name="workToPerform">A Task object repenting an operation over the given document.</param>
        /// <param name="initializationMessage">
        /// A message indicating the start of specific the ProcessingTask.
        /// </param>
        /// <param name="completionMessage">A message indicating the end of specific the ProcessingTask.</param>
        /// <param name="percentWorkRepresented">
        /// An arbitrary double value corresponding to a relative amount of work the ProcessingTask represents.
        /// </param>
        public ProcessingTask(
            Task workToPerform,
            string initializationMessage,
            string completionMessage,
            double percentWorkRepresented)
        {
            Task = workToPerform;
            InitializationMessage = initializationMessage;
            CompletionMessage = completionMessage;
            PercentCompleted = percentWorkRepresented;
        }

        /// <summary>
        /// Initializes a new Instance of the Processing Task class with the given Action,
        /// initialization message, completion message, and percentage of total work represented.
        /// </summary>
        /// <param name="workToPerform">A Task object repenting an operation over the given document.</param>
        /// <param name="initializationMessage">
        /// A message indicating the start of specific the ProcessingTask.
        /// </param>
        /// <param name="completionMessage">A message indicating the end of specific the ProcessingTask.</param>
        /// <param name="percentWorkRepresented">
        /// An arbitrary double value corresponding to a relative amount of work the ProcessingTask represents.
        /// </param>
        public ProcessingTask(Action workToPerform,
            string initializationMessage,
            string completionMessage,
            double percentWorkRepresented)
            : this(Task.Run(workToPerform),
            initializationMessage,
            completionMessage,
            percentWorkRepresented)
        { }

        public void Dispose()
        {
            ((IDisposable)this.Task).Dispose();
        }

        /// <summary>
        /// Gets a message indicating the end of specific the ProcessingTask.
        /// </summary>
        public string CompletionMessage { get; }

        /// <summary>
        /// Gets a message indicating the start of specific the ProcessingTask.
        /// </summary>
        public string InitializationMessage { get; }

        /// <summary>
        /// Gets a double value corresponding to the relative amount of work the ProcessingTask represents.
        /// </summary>
        public double PercentCompleted { get; }

        /// <summary>
        /// Gets the work the ProcessingTask will perform.
        /// </summary>
        public Task Task { get; }
    }
}