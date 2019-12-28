using System;

namespace SharpLoad.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string helpText = "-f [configFilePath] - Runs a test based on a JSON config file test \n"
                                + "-p [path] - Runs a test on quick test on a specified url, only GET requests \n"
                                + "-u [maxUsers] - Max load users being simulated \n"
                                + "-s [spawnRate] - Spawn user rate per second \n"
                                + "-H [host] - Host of master node\n\n\n"
                                + "--master - Runs the application in master mode for distributed Loading Tests \n"
                                + "--slave - Runs the application in slave mode for distributed Load Tests \n";
                                
            Console.WriteLine("\n\n=================================== SharpLoad - A .NET Core Load Testing CLI tool ===================================\n\n");
            
            CommandLineParser parsedOptions = new CommandLineParser(args);
                        
            if (parsedOptions.ParsedParams.ContainsKey("--help") || parsedOptions.ParsedParams.ContainsKey("-h") || parsedOptions.ParsedParams.Count == 0)
                Console.WriteLine(helpText);
        }
    }
}
