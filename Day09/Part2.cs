using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2023.Day09
{
    internal class Part2
    {
        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day09\\input.txt";
            StreamReader reader = new StreamReader(filePath);

            string line;
            int sum = 0;

            while ((line = reader.ReadLine()) != null)
            {
                List<int> numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).Reverse().ToList();
                sum += devourList(numbers, new List<int>()).Sum();
            }

            Console.WriteLine(sum);
        }


        static List<int> devourList(List<int> lst, List<int> lastNumbers)
        {
            List<int> diffList = new List<int>();

            for (int i = 0; i < lst.Count - 1; i++)
            {
                diffList.Add(lst[i + 1] - lst[i]);
            }

            if (lst.Count > 0)
            {
                lastNumbers.Add(lst.Last());
            }

            lst = diffList;

            if (lst.All(x => x == 0))
            {
                return lastNumbers;
            }
            else
            {
                return devourList(lst, lastNumbers);
            }
        }
    }
}
