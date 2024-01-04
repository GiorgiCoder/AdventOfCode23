using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2023.Day08
{
    internal class Part1
    {
        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day08\\input.txt";
            StreamReader reader = new StreamReader(filePath);
            string[] all = reader.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            List<char> instructions = all[0].ToList();
            Dictionary<string, (string, string)> mappings = new();

            string pattern = @"(\w+)\s*=\s*\((\w+),\s*(\w+)\)";

            for (int i = 1; i < all.Length; i++)
            {
                Match match = Regex.Match(all[i], pattern);
                mappings.Add(match.Groups[1].Value, (match.Groups[2].Value, match.Groups[3].Value));
            }
            var copyOfInstructions = new List<char>(instructions);


            string findString = "AAA";
            int countOfSteps = 0;

            while (!findString.Equals("ZZZ"))
            {
                var lr = mappings[findString];
                if(copyOfInstructions.Count == 0) { copyOfInstructions = new List<char>(instructions); }
                char currentInstruction = copyOfInstructions[0];
                copyOfInstructions.RemoveAt(0);
                if (currentInstruction.Equals('L'))
                {
                    string a = mappings[findString].Item1;
                    findString = a;
                }
                else
                {
                    string a = mappings[findString].Item2;
                    findString = a;
                }

                countOfSteps++;
            }

            Console.WriteLine(countOfSteps);
        }
    }
}
