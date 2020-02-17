using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace m3u8_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Url: ");
                Uri url = new Uri(Console.ReadLine());
                string baseUrl = url.OriginalString.Substring(0, url.OriginalString.LastIndexOf("/"));
                string output = "";

                using (WebClient webClient = new WebClient())
                {
                    string m3u8file = webClient.DownloadString(url);
                    foreach (string line in m3u8file.Split("\n"))
                    {
                        if (!line.StartsWith("#") && !string.IsNullOrWhiteSpace(line))
                        {
                            output += baseUrl + "/" + line + "\n";
                        }
                    }
                }

                using (FileStream fStream = File.OpenWrite(name + ".txt")) fStream.Write(Encoding.UTF8.GetBytes(output));
            }
        }
    }
}
