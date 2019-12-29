using Newtonsoft.Json;
using SharpLoad.Application.Models;
using System;
using System.IO;

namespace SharpLoad.Application
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            string helpText = "-f [configFilePath] - Runs a test based on a JSON config file test \n"
                                + "-p [path] - Runs a test on quick test on a specified url, only GET requests \n"
                                + "-u [maxUsers] - Max load users being simulated \n"
                                + "-s [spawnRate] - Spawn user rate per second \n"
                                + "-H [host] - Host of master node\n\n\n"
                                + "--master - Runs the application in master mode for distributed Loading Tests \n"
                                + "--slave - Runs the application in slave mode for distributed Load Tests \n"
                                + "--verbose - Shows detailed information on running program"
                                + "-l [logLevel] - 0 = Default, 1 = Errors and Warnings, 2 = Full";

            Console.WriteLine("\n\n =================================== SharpLoad - A .NET Core Load Testing CLI tool ===================================\n\n");

            try
            {
                CommandLineParser parsedOptions = new CommandLineParser(args);

                if (parsedOptions.ParsedParams.ContainsKey("--help") || parsedOptions.ParsedParams.ContainsKey("-h") || parsedOptions.ParsedParams.Count == 0)
                {
                    Console.WriteLine(helpText);
                }
                else if (parsedOptions.ParsedParams.ContainsKey("-f"))
                {
                    TestScript testScript = JsonConvert.DeserializeObject<TestScript>(LoadJson(parsedOptions.ParsedParams["-f"]));
                    TestRunner runner = new TestRunner(testScript);
                    bool verbose = parsedOptions.ParsedParams.ContainsKey("--verbose") ? true : false;
                    uint logLevel = parsedOptions.ParsedParams.ContainsKey("-l") ? uint.Parse(parsedOptions.ParsedParams["-l"]) : 0;
                    runner.RunTests(verbose);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static string LoadJson(string filePath)
        {
            string jsonStringContent = string.Empty;

            if (File.Exists(filePath) && filePath.ToLower().EndsWith(".json"))
            {
                using (StreamReader s = new StreamReader(File.OpenRead(filePath)))
                {
                    jsonStringContent = s.ReadToEnd();
                    return jsonStringContent;
                }
            }
            else
            {
                throw new FileLoadException($"File not found at {jsonStringContent} or file is not *.json format");
            }
        }
    }
}
