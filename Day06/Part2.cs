using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2023.Day06
{
    internal class Part2
    {
        static void main(string[] args)
        {
            string inputExample = "Time:      7  15   30\r\nDistance:  9  40  200";
            string input = "Time:        57     72     69     92\r\nDistance:   291   1172   1176   2026";

            string timesString = input.Split("\r\n")[0].Split(":")[1].Trim();
            string distancesString = input.Split("\r\n")[1].Split(":")[1].Trim();

            long time = long.Parse(timesString.Replace(" ", ""));
            long distance = long.Parse(distancesString.Replace(" ", ""));

            int count = 0;

            for(long i = 1; i <= time; i++)
            {
                if(i*(time-i) > distance)
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }
    }
}
