// In the C# file, write a program to perform a GET request on the route https://coderbyte.com/api/challenges/logs/web-logs-raw which contains a portion of web server logs. Each line begins with a date, e.g. Apr 10 11:17:35. Your program should do the following:

// Some of the log entries have a string that contains ?shareLinkId=[ID]. You should collect all of the unique ID's from that string and return all of them in string format with each ID on its own line. If an ID appears more than once, append :N to the end of the ID where N is the number of times it appears.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace WebLogsParser
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://coderbyte.com/api/challenges/logs/web-logs-raw");
            var content = await response.Content.ReadAsStringAsync();
            var lines = content.Split('\n');
            var shareLinkIds = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                var match = Regex.Match(line, "\\?shareLinkId=(\\d+)");
                if (match.Success)
                {
                    var id = match.Groups[1].Value;
                    if (shareLinkIds.ContainsKey(id))
                    {
                        shareLinkIds[id]++;
                    }
                    else
                    {
                        shareLinkIds[id] = 1;
                    }
                }
            }

            foreach (var kvp in shareLinkIds)
            {
                Console.WriteLine($"{kvp.Key}{(kvp.Value > 1 ? $":{kvp.Value}" : "")}");
            }
        }
    }
}