using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SharpLoad.Application
{
    public class CommandLineParser
    {
        private readonly Regex paramsRegex;

        public readonly IDictionary<string, string> ParsedParams;

        public CommandLineParser(string[] args)
        {
            string allParamsFromCli = string.Empty;
            ParsedParams = new Dictionary<string, string>();

            foreach (string p in args)
                allParamsFromCli += p + " ";

            paramsRegex = new Regex(@"(--help|-h\b)|(--master)|(--slave)|(-p\s\S+)(-u\s\S+)|(-H\s\S+)|(-s\s\S+)|(-f\s\'.*?\')|(-f\s\S+)", RegexOptions.ECMAScript);
            MatchCollection matches = paramsRegex.Matches(allParamsFromCli);

            foreach (var m in matches)
            {
                string currentString = m.ToString();
                if (currentString.Length > 0)
                {
                    if (currentString.Contains("-f") && currentString.EndsWith("'"))
                    {
                        string[] nameValueSplit = new string[2] { currentString.Substring(0, 2), currentString.Substring(3, currentString.Length - 3).Replace("'", string.Empty) };
                        if (nameValueSplit.Length == 2)
                            ParsedParams.TryAdd(nameValueSplit[0], nameValueSplit[1]);
                        else if (nameValueSplit.Length == 1)
                            ParsedParams.TryAdd(nameValueSplit[0], nameValueSplit[0]);
                    }
                    else
                    {
                        string[] nameValueSplit = currentString.Split(' ');
                        if (nameValueSplit.Length == 2)
                            ParsedParams.TryAdd(nameValueSplit[0], nameValueSplit[1]);
                        else if (nameValueSplit.Length == 1)
                            ParsedParams.TryAdd(nameValueSplit[0], nameValueSplit[0]);
                    }
                }
            }
        }

        //To bo user in future in a Nuget Pacckage maybe

        //public CommandLineParser(IList<string> availableOptions, string[] args)
        //{
        //    string allParamsFromCli = string.Empty;
        //    string paramsRegexString = string.Empty;
        //    ParsedParams = new Dictionary<string, string>();

        //    foreach (string p in args)
        //        allParamsFromCli += p + " ";

        //    foreach (string o in availableOptions)
        //        paramsRegexString += "(" + o + ")|";
            
        //    paramsRegexString.TrimEnd('|');
            
        //    paramsRegex = new Regex(@paramsRegexString);
        //    MatchCollection matches = paramsRegex.Matches(allParamsFromCli);

        //    foreach (var m in matches)
        //    {
        //        if (m.ToString().Length > 0)
        //        {
        //            string[] nameValueSplit = m.ToString().Split(' ');
        //            if (nameValueSplit.Length == 2)
        //                ParsedParams.TryAdd(nameValueSplit[0], nameValueSplit[1]);
        //            else if (nameValueSplit.Length == 1)
        //                ParsedParams.TryAdd(nameValueSplit[0], nameValueSplit[0]);
        //        }
        //    }
        //}
    }
}
