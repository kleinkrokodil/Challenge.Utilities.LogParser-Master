namespace Challenge.Utilities.LogParser.Interfaces
{
    using Challenge.Utilities.LogParser.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// This is the interface for the log parser
    /// </summary>
    interface ILogParser
    {
        void ParseLogFile(string inputPath);

        int TotalUniqueIpAddresses();

        List<LogItemTotal> GetTopIpAddresses(int itemCount);

        List<LogItemTotal> GetTopUrls(int itemCount);
    }
}
