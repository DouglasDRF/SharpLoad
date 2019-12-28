using System;
using System.Collections.Generic;
using Xunit;
using SharpLoad.Application;


namespace SharpLoad.UnitTests
{
    public class CommandParseTest
    {
        [Fact]
        public void ParseParams()
        {
            IList<string[]> testCases = new List<string[]>() {
                new string[] { "-f", @"C:\NoSpaceFolder\NoSpaceUser\file.json" },
                new string[] { "-f", @"'C:\With Space Folder\With Space User\file.json'" },
                new string[] { "-f", @"'C:\With Space Folder\With Space User\file.json'", "--master" },
                new string[] { "--slave", "-H", "127.0.0.1" },
                Array.Empty<string>(),
            };

            CommandLineParser options = new CommandLineParser(testCases[0]);
            Assert.True(options.ParsedParams.ContainsKey("-f") && options.ParsedParams["-f"] == @"C:\NoSpaceFolder\NoSpaceUser\file.json");

            options = new CommandLineParser(testCases[1]);
            Assert.True(options.ParsedParams.ContainsKey("-f") && options.ParsedParams["-f"] == @"C:\With Space Folder\With Space User\file.json");

            options = new CommandLineParser(testCases[2]);
            Assert.True(options.ParsedParams.ContainsKey("-f") && options.ParsedParams["-f"] == @"C:\With Space Folder\With Space User\file.json" && options.ParsedParams.ContainsKey("--master"));

            options = new CommandLineParser(testCases[3]);
            Assert.True(options.ParsedParams.ContainsKey("--slave") && options.ParsedParams.ContainsKey("-H") && options.ParsedParams["-H"] == @"127.0.0.1");

            options = new CommandLineParser(testCases[4]);
            Assert.True(options.ParsedParams.Count == 0);

        }
    }
}
