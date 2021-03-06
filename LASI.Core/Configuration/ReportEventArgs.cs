﻿using System;
using System.Runtime.InteropServices;

namespace LASI.Core.Configuration
{
    /// <summary>
    /// Contains numeric and textual data related to an event.
    /// </summary>
    public abstract class ReportEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ReportEventArgs class.
        /// </summary>
        /// <param name="message">The phase of analysis currently underway.</param>
        /// <param name="percentWorkRepresented">The percent of overall progress completed.</param>
        protected ReportEventArgs(string message, double percentWorkRepresented)
        {
            Message = message;
            PercentWorkRepresented = percentWorkRepresented;
        }
        /// <summary>
        /// Gets a message indicating the phase of analysis underway when they Report was created.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Gets a value indicating the amount of overall progress.
        /// </summary>
        public double PercentWorkRepresented { get; set; }
    }
}
