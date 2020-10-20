namespace Challenge.Utilities.LogParser.Models
{

using System;
using System.Collections.Generic;
using System.Text;

    /// <summary>
    /// This model contains properties to store a group name as well at the times it occurs in the log file.
    /// </summary>
    public class LogItemTotal
    {
        /// <summary>
        /// The LogItemGroup is either the IP Address or URL.
        /// </summary>
        public string LogItemGroup { get; set; }

        /// <summary>
        /// The TotalGroup stores the times the LogItemGroup occurs in the log file.
        /// </summary>
        public int TotalGroup { get; set; }
    }
}
