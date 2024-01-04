using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode.AdventOfCode2023.Day04
{
    internal class Part2
    {

        static Dictionary<int, int> IdAndCount = new Dictionary<int, int>();

        public static void main(string[] args)
        {

            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day04\\input.txt";

            StreamReader reader = new StreamReader(filePath);

            string[] allLines = reader.ReadToEnd().Split('\n');

            for (int i = 0; i < allLines.Length; i++)
            {
                getWinningNumbers(allLines[i]);
            }

            Console.WriteLine(allLines.Length + countTotalCards(IdAndCount));

        }


        static void getWinningNumbers(string line)
        {
            string[] gameAndNumbers = line.Split(": ");
            int gameID = int.Parse(gameAndNumbers[0].Split(" ").Last());
            List<int> winningNumbers = stringToIntList(gameAndNumbers[1].Split(" | ")[0].Trim());
            List<int> myNumbers = stringToIntList(gameAndNumbers[1].Split(" | ")[1].Trim());
            int winningNumbersCount = countWinningNumbers(winningNumbers, myNumbers);

            IdAndCount.Add(gameID, winningNumbersCount);

        }

        static int countTotalCards(Dictionary<int, int> dict)
        {
            return 0;
        }


        public static List<int> stringToIntList(string s)
        {
            return s.Split(" ").Where(x => !x.Equals("")).Select(int.Parse).ToList();
        }

        public static int countWinningNumbers(List<int> winningNums, List<int> myNums)
        {
            int count = 0;
            foreach (int i in myNums)
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

//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;

//namespace AdventOfCode.AdventOfCode2023.Day04
//{
//    internal class Part2
//    {
//        public static int totalCards = 0;

//        static string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day04\\input.txt";

//        static StreamReader reader = new StreamReader(filePath);

//        static string[] allLines = reader.ReadToEnd().Split('\n');

//        static Dictionary<int, int> IdAndCount = new Dictionary<int, int>();

//        public static void Main(string[] args)
//        {
//            //var v1 = new Stopwatch();
//            //v1.Start();


//            for (int i = 0; i < allLines.Length; i++)
//            {
//                totalCards++;
//                getWinningNumbers(allLines[i]);
//            }
//            Console.WriteLine(totalCards);

//            //v1.Stop();
//            //Console.WriteLine(v1.ElapsedMilliseconds);
//        }

//        static void getWinningNumbers(string line)
//        {
//            string[] gameAndNumbers = line.Split(": ");
//            int gameID = int.Parse(gameAndNumbers[0].Split(" ").Last());
//            List<int> winningNumbers = stringToIntList(gameAndNumbers[1].Split(" | ")[0].Trim());
//            List<int> myNumbers = stringToIntList(gameAndNumbers[1].Split(" | ")[1].Trim());
//            int winningNumbersCount = countWinningNumbers(winningNumbers, myNumbers);
//            totalCards += winningNumbersCount;


//            for (int j = gameID; j < gameID + winningNumbersCount; j++)
//            {
//                getWinningNumbers(allLines[j]);
//            }
//        }

//        public static List<int> stringToIntList(string s)
//        {
//            return s.Split(" ").Where(x => !x.Equals("")).Select(int.Parse).ToList();
//        }

//        public static int countWinningNumbers(List<int> winningNums, List<int> myNums)
//        {
//            int count = 0;
//            foreach (int i in myNums)
//            {
//                if (winningNums.Contains(i))
//                {
//                    count++;
//                }
//            }
//            return count;
//        }
//    }
//}



