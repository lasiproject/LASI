using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Associates a Task, the Document to which it applies, and initialization and completion
    /// feedback properties.
    /// </summary>
    public sealed class ProcessingTask : IDisposable, IEquatable<ProcessingTask>
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

        /// <summary>
        ///  Releases all resources used by the current instance of the underlying <see cref="System.Threading.Tasks.Task"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The exception that is thrown if the System.Threading.Tasks.Task is not in one
        /// of the final states: <see cref="TaskStatus.RanToCompletion"/>, <see cref="TaskStatus.Faulted"/>,
        /// or <see cref="TaskStatus.Canceled"/>.
        /// </exception>
        public void Dispose()
        {
            ((IDisposable)Task).Dispose();
            GC.SuppressFinalize(this);
        }
        public override bool Equals(object obj) => obj is ProcessingTask t && Equals(t);
        public bool Equals(ProcessingTask other)
        {
            return !(other is null) && ReferenceEquals(this, other) ? true : Equals(Task, other?.Task);
        }
        /// <summary>
        /// Gets an awaiter used to await the underlying <see cref="System.Threading.Tasks.Task"/>.
        /// </summary>
        /// <returns>An awaiter instance.</returns>
        public TaskAwaiter GetAwaiter() => Task.GetAwaiter();

        /// <summary>Serves as the default hash function. </summary>
        /// <returns>A hash code for the current <see cref="ProcessingTask"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 47;
                hashCode ^= Task.GetHashCode();
                return hashCode;
            }


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
        /// The work the ProcessingTask will perform.
        /// </summary>
        public Task Task { get; }

        public static bool operator !=(ProcessingTask first, ProcessingTask second) => !(first == second);
        public static bool operator ==(ProcessingTask first, ProcessingTask second)
        {
            if ((object)first == null)
            {
                return (object)second == null;
            }

            return first.Equals(second);
        }
        public static implicit operator Task(ProcessingTask processingTask) => processingTask.Task;
    }
}