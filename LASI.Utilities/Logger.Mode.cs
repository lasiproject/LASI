namespace LASI.Utilities
{
    public static partial class Logger
    {
        /// <summary>
        /// Defines the output modes of the Output class.
        /// </summary>
        public enum Mode
        {
            /// <summary>
            /// The default. An output mode has not yet been set.
            /// </summary>
            Unspecified,
            /// <summary>
            /// Output will be directed to the current console window.
            /// </summary>
            /// <seealso cref="Logger.SetToConsole"/>
            Console,
            /// <summary>
            /// Output will be directed to the IDE's debug window.
            /// </summary>
            /// <seealso cref="Logger.SetToDebug"/>
            Debug,
            /// <summary>
            /// No output will occur.
            /// </summary>
            /// <seealso cref="Logger.SetToSilent"/>
            Silent,
            /// <summary>
            /// Output will be directed to a file.
            /// </summary>
            /// <seealso cref="Logger.SetToFile(string)" />
            File,
            /// <summary>
            /// Output will be directed to an externally supplied destination.
            /// </summary>
            /// <seealso cref="Logger.SetTo(System.IO.TextWriter)"/>
            Custom
        }
    }
}