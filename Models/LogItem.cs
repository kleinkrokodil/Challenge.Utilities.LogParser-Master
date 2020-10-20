namespace Challenge.Utilities.LogParser.Models
{

using System;
using System.Collections.Generic;
using System.Text;

    /// <summary>
    /// This model contains properties to store the IpAddress and URL for a log entry.
    /// </summary>
    public class LogItem
    {
        /// <summary>
        /// The IP address related to the log entry. This is the first value found in the log file.
        /// </summary>
        public string logIpAddress { get; set; }

        /// <summary>
        /// The URL related to the log entry
        /// </summary>
        public string logUrl { get; set; }

    }
}
