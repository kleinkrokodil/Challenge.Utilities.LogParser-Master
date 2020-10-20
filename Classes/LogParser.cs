namespace Challenge.Utilities.LogParser
{
    using Challenge.Utilities.LogParser.Interfaces;
    using Challenge.Utilities.LogParser.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// This is a parser for the log file containing HTTP requests
    /// </summary>
    public class LogParser : ILogParser
    {
        List<LogItem> logitemList = new List<LogItem>();

        /// <summary>
        /// This method parses the log file containing the HTTP requests
        /// </summary>
        /// <param name="inputPath">This is the path to the log file which need to be parsed</param>
        public void ParseLogFile(string inputPath)
        {

            logitemList = new List<LogItem>();

            try
            {
                using (StreamReader sr = File.OpenText(inputPath)) //A stream reader is used to avoid running out of system memory
                {
                    String line = "";

                    while ((line = sr.ReadLine()) != null) //Keep executing until all entries have been read. 
                    {

                        LogItem item = new LogItem(); //Create a log item object so that we can add it to a collection.

                        Match IpMatch = Regex.Match(line, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b"); //Using RegEx to find the values of interest as the values are not in a fixed location. This is a pattern match for the IP Address.

                        if ((IpMatch.Success))
                        {
                            item.logIpAddress = IpMatch.Value; //Assign value to address property of LogItem object
                        }

                        Match UrlMatch = Regex.Match(line, "GET(.*)HTTP", RegexOptions.IgnoreCase); //This RegEx is to extract the URL for the log entry.

                        if ((UrlMatch.Success))
                        {
                            item.logUrl = UrlMatch.Groups[1].Value; //Assign value to URL property of LogItem object
                        }

                        logitemList.Add(item); //Add LogItem object to collection

                    }
                }
            }
            catch (FileNotFoundException)
            {
                throw new Exception("File not found");
            }
            catch (OutOfMemoryException)
            {
                throw new Exception("No system memory");
            }
            catch (IOException)
            {
                throw new Exception("Cannot stream file");
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error. " + ex.Message);
            }
        }

        /// <summary>
        /// This method returns the total unique IP addresses found in the log file.
        /// </summary>
        /// <returns>An integer is returned containing the amount of unique addresses in the file.</returns>
        public int TotalUniqueIpAddresses()
        {
            return logitemList.Select(x => x.logIpAddress).Distinct().Count();
        }

        /// <summary>
        /// This method returns the top most active IP addresses
        /// </summary>
        /// <param name="itemCount">Specify the amount of records to be returned</param>
        /// <returns></returns>
        public List<LogItemTotal> GetTopIpAddresses(int itemCount)
        {

            return logitemList.GroupBy(t => t.logIpAddress)
               .Select(t => new LogItemTotal
               {
                   LogItemGroup = t.Key,
                   TotalGroup = t.Count()
               }).OrderByDescending(o => o.TotalGroup).Take(itemCount).ToList();

        }

        /// <summary>
        /// This method returns the top most visited URLs
        /// </summary>
        /// <param name="itemCount">Specify the amount of records to be returned</param>
        /// <returns>A list containing log item totals is returned</returns>
        public List<LogItemTotal> GetTopUrls(int itemCount)
        {

            return logitemList.GroupBy(t => t.logUrl)
               .Select(t => new LogItemTotal
               {
                   LogItemGroup = t.Key,
                   TotalGroup = t.Count()
               }).OrderByDescending(o => o.TotalGroup).Take(itemCount).ToList();

        }
    }
}
