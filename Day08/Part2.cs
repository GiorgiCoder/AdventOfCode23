using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2023.Day08
{
    internal class Part2
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
            string[] startingStrings = mappings.Select(s => s.Key).Where(s => s.EndsWith('A')).ToArray();

            List<int> allStepCounts = new List<int>();

            for(int i = 0; i < startingStrings.Length; i++)
            {
                int countOfSteps = 0;
                while (!startingStrings[i].EndsWith('Z'))
                {
                    var lr = mappings[startingStrings[i]];
                    if (copyOfInstructions.Count == 0) { copyOfInstructions = new List<char>(instructions); }
                    char currentInstruction = copyOfInstructions[0];
                    copyOfInstructions.RemoveAt(0);
                    if (currentInstruction.Equals('L'))
                    {
                        string a = mappings[startingStrings[i]].Item1;
                        startingStrings[i] = a;
                    }
                    else
                    {
                        string a = mappings[startingStrings[i]].Item2;
                        startingStrings[i] = a;
                    }
                    countOfSteps++;
                }

                allStepCounts.Add(countOfSteps);

            }

            Console.WriteLine(FindLcm(allStepCounts));

        }

        static long Gcd(long a, long b)
        {
            while (b != 0)
            {
                long t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        static long Lcm(long a, long b)
        {
            return (a / Gcd(a, b)) * b;
        }

        static long FindLcm(List<int> numbers)
        {
            long result = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                result = Lcm(result, numbers[i]);
            }
            return result;
        }

    }
}
