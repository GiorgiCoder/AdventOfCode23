using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2023.Day21
{
    internal class Part1
    {
        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day21\\input.txt";
            StreamReader reader = new StreamReader(filePath);

            string[] lines = reader.ReadToEnd().Split("\r\n");

            int rows = lines.Length;
            int columns = lines[0].Length;

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

            int rowOfS = 0, colOfS = 0;

            // Copying the original array
            for (int i = 1; i < rows + 1; i++)
            {
                char[] charsOfLine = lines[i - 1].ToCharArray();
                for (int j = 1; j < columns + 1; j++)
                {
                    map[i, j] = charsOfLine[j - 1];
                    if (map[i, j] == 'S')
                    {
                        rowOfS = i;
                        colOfS = j;
                    }
                }
            }

            Queue<(int, int)> possibleGardens = new Queue<(int, int)>();
            possibleGardens.Enqueue((rowOfS - 1, colOfS));
            possibleGardens.Enqueue((rowOfS, colOfS + 1));
            possibleGardens.Enqueue((rowOfS + 1, colOfS));
            possibleGardens.Enqueue((rowOfS, colOfS - 1));
            int steps = 1;

            while (possibleGardens.Count > 0 && steps < 64)
            {
                int currentCount = possibleGardens.Count;

                for (int j = 0; j < currentCount; j++)
                {
                    var (x, y) = possibleGardens.Dequeue();

                    if (map[x - 1, y] != '#') 
                    { 
                        if(!possibleGardens.Contains((x - 1, y)))
                            possibleGardens.Enqueue((x - 1, y)); 
                    }
                    if (map[x, y + 1] != '#')
                    {
                        if (!possibleGardens.Contains((x, y + 1)))
                            possibleGardens.Enqueue((x, y + 1));
                    }
                    if (map[x + 1, y] != '#')
                    {
                        if (!possibleGardens.Contains((x + 1, y)))
                            possibleGardens.Enqueue((x + 1, y));
                    }
                    if (map[x, y - 1] != '#')
                    {
                        if (!possibleGardens.Contains((x, y - 1)))
                            possibleGardens.Enqueue((x, y - 1));
                    }
                }

                steps++;
            }

            Console.WriteLine(possibleGardens.Count());

        }
    }
}
