namespace AdventOfCode.Advent_Of_Code_2023.Day_1
{
    internal class Part1
    {
        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day01\\input.txt";

            Console.WriteLine(count(filePath));
        }

        static int count(string filePath)
        {
            int sum = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                   int number = getNumber(line);
                   sum += number;
                }
            }

            return sum;
        }

        static int getNumber(string s)
        {
            List<int> numbers = new List<int>();
            foreach (char c in s)
            {
                if (char.IsDigit(c))
                {
                    numbers.Add(c - '0');
                }
            }

            return numbers.First() * 10 + numbers.Last();
        }
    }
}
