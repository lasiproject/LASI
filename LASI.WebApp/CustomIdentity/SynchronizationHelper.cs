using System;
using System.Threading;
using System.Threading.Tasks;

namespace LASI.WebApp.CustomIdentity
{
    public static class SynchronizationHelper
    {
        public static async Task IfNotCancelled(this CancellationToken cancellationToken, Action action)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Run(action);
        }
        public static async Task<TResult> IfNotCancelled<TResult>(this CancellationToken cancellationToken, Func<TResult> function)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await Task.Run(function);
        }
        public static async Task<TResult> IfNotCancelled<TResult>(this CancellationToken cancellationToken, Func<Task<TResult>> taskOfTResult)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await taskOfTResult();
        }
        public static async Task IfNotCancelled(this CancellationToken cancellationToken, Func<Task> task)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await task();
        }
    }
}