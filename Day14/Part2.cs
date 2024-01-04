// There is no actual answer here in the code, cuz 1_000_000_000 cycles would take days (153.57 days to be exact, yeah I counted :D)
// I printed the result of first 1000 cycles and noticed that starting from 99th cycle,
// it was a loop of length 36. (1_000_000_000 - 98) mod 36 = 999999902 mod 36 = 2.
// So, the answer is the second element of this loop, 97241 :))

using System.Diagnostics;

namespace AdventOfCode.AdventOfCode2023.Day14
{
    internal class Part2
    {

        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day14\\input.txt";
            StreamReader reader = new StreamReader(filePath);

            string[] allLines = reader.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            int rows = allLines.Length;
            int columns = allLines[0].Length;

            char[,] map = new char[rows + 2, columns + 2];

            // Filling the borders with #
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < columns + 2; j++)
                {
                    map[i, j] = '#';
                }
            }
            for (int i = rows + 1; i < rows + 2; i++)
            {
                for (int j = 0; j < columns + 2; j++)
                {
                    map[i, j] = '#';
                }
            }
            for (int i = 0; i < rows + 2; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    map[i, j] = '#';
                }
            }
            for (int i = 0; i < rows + 2; i++)
            {
                for (int j = columns + 1; j < columns + 2; j++)
                {
                    map[i, j] = '#';
                }
            }

            // Copying the original array
            for (int i = 1; i < rows + 1; i++)
            {
                char[] charsOfLine = allLines[i - 1].ToCharArray();
                for (int j = 1; j < columns + 1; j++)
                {
                    map[i, j] = charsOfLine[j - 1];
                }
            }


            int result = 0;
            int cycles = 1000;
            int n = 0;

            while (n < 4 * cycles)
            {
                result = 0;
                for (int i = 1; i < map.GetLength(0); i++)
                {
                    for (int k = i; k >= 1; k--)
                    {
                        for (int j = 1; j < map.GetLength(1); j++)
                        {
                            if (map[k, j] == 'O' && map[k - 1, j] == '.')
                            {
                                map[k, j] = '.';
                                map[k - 1, j] = 'O';
                            }
                        }
                    }
                }

                map = rotateArray(map);

                n++;

                if (n % 4 == 0)
                {
                    for (int i = 1; i < rows + 1; i++)
                    {
                        for (int j = 1; j < columns + 1; j++)
                        {
                            if (map[i, j] == 'O')
                            {
                                result += rows + 1 - i;
                            }
                        }
                    }
                    Console.WriteLine($"{n / 4}: {result}");
                }
            }

            Console.WriteLine(97241);
        }

        public static char[,] rotateArray(char[,] oldArray)
        {
            int rows = oldArray.GetLength(0);
            int cols = oldArray.GetLength(1);
            char[,] result = new char[cols, rows];

            for (int oldRow = 0; oldRow < rows; oldRow++)
            {
                for (int oldCol = 0; oldCol < cols; oldCol++)
                {
                    result[oldCol, rows - 1 - oldRow] = oldArray[oldRow, oldCol];
                }
            }

            return result;
        }
    }
}