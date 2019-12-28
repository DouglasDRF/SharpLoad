using SharpLoad.Client;
using System;

namespace SharpLoad.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=================================== SharpLoad - Load Testing CLI tool ===================================");

            CommandLineParser parsedOptions = new CommandLineParser(args);
            CommandLineParser customaParsedOptions = new CommandLineParser(args);

            //LoadTestClient Client = LoadTestClient.GetInstance(host);

            //Console.WriteLine($"Initializing Load Test at {host} with {maxUsers} Maximum Active Users and with {spawnRate} Spawning Rate");
            //Console.WriteLine($"Testing Script at {jsonFilePath}");

        }
    }
}
