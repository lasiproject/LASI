namespace LASI.App.Dialogs
{
    /// <summary>
    /// Delineates the categories of errors which are displayed to the user.
    /// </summary>
    public enum ErrorKind
    {
        /// <summary>
        /// A default error, equivalent to <see cref="NonFatal"/>.
        /// </summary>
        Default,
        /// <summary>
        /// A non fatal error from which the application can recover.
        /// </summary>
        NonFatal = Default,
        /// <summary>
        /// An from which the application cannot recover.
        /// </summary>
        Fatal
    }
}