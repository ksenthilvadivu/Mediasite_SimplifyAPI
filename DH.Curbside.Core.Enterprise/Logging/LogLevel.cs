namespace DH.Curbside.Core.Enterprise.Logging
{
    /// <summary>
    /// Enumeration to represent log levels. <remarks>This is used to classify log output "verbosity"</remarks>
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Debug is the most verbose logging level. This is intended for troubleshooting in a dev/qa environment.
        /// </summary>
        Debug = 0,

        /// <summary>
        /// Information is the logging level production is usually set to.
        /// </summary>
        Information = 1,

        /// <summary>
        /// Level for non-fatal conditions.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Error/exception level.
        /// </summary>
        Error = 3,

        /// <summary>
        /// Fatal error (fax failover not failures...etc.).
        /// </summary>
        Fatal = 4
    }
}
