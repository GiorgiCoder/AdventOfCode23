using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2023.Day04
{
    internal class Part1
    {
        public static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day04\\input.txt";

            int sumOfPoints = 0;

            StreamReader reader = new StreamReader(filePath);

            string line;

            while((line = reader.ReadLine()) != null)
            {
                string[] gameAndNumbers = line.Split(": ");
                int gameID = int.Parse(gameAndNumbers[0].Split(" ").Last());
                List<int> winningNumbers = stringToIntList(gameAndNumbers[1].Split(" | ")[0].Trim());
                List<int> myNumbers = stringToIntList(gameAndNumbers[1].Split(" | ")[1].Trim());

                int points = (int)Math.Pow(2, countWinningNumbers(winningNumbers, myNumbers) - 1);
                sumOfPoints += points;
            }

            Console.WriteLine(sumOfPoints);
        }

        public static List<int> stringToIntList(string s)
        {
            return s.Split(" ").Where(x => !x.Equals("")).Select(int.Parse).ToList();
        }
       
        public static int countWinningNumbers(List<int> winningNums, List<int> myNums)
        {
            int count = 0;
            foreach(int i in myNums)
            {
                if (winningNums.Contains(i))
                {
                    count++;
                }
            }
            return count;
        }
    }
}
