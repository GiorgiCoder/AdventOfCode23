using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2023.Day06
{
    internal class Part1
    {
        static void main(string[] args)
        {
            string inputExample = "Time:      7  15   30\r\nDistance:  9  40  200";
            string input = "Time:        57     72     69     92\r\nDistance:   291   1172   1176   2026";

            string timesString = input.Split("\r\n")[0].Split(":")[1].Trim();
            string distancesString = input.Split("\r\n")[1].Split(":")[1].Trim();

            int[] times = timesString.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
            int[] distances = distancesString.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();

            int result = 1;

            for(int i = 0; i < times.Length; i++)
            {
                int count = 0;
                for(int j = 1; j < times[i]; j++)
                {
                    if( j * (times[i] - j) > distances[i])
                    {
                        count++;
                    }
                }
                result *= count;
            }

            Console.WriteLine(result);
        }
        
    }
}
