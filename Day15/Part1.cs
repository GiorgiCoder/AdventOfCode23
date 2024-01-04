using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2023.Day15
{
    internal class Part1
    {
        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day15\\input.txt";
            StreamReader reader = new StreamReader(filePath);

            string[] strings = reader.ReadToEnd().Split(',');

            int sumOfResults = 0;
            int result = 0;

            for(int i = 0; i < strings.Length; i++)
            {
                char[] chars = strings[i].ToCharArray();

                for (int j = 0; j < chars.Length; j++)
                {
                    result += chars[j].GetHashCode();
                    result *= 17;
                    result = result % 256;
                }
                sumOfResults += result;
            }

            Console.WriteLine(sumOfResults);
        }
    }
}
