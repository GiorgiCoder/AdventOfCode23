using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2023.Day11
{
    internal class Part2
    {
        public static char[,] initialUniverse;
        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day11\\input.txt";
            StreamReader reader = new StreamReader(filePath);

            string[] lines = reader.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            int rows = lines.Length;
            int cols = lines[0].Length;
            char[,] _initialUniverse = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                char[] charsOfLine = lines[i].ToCharArray();

                for (int j = 0; j < cols; j++)
                {
                    _initialUniverse[i, j] = charsOfLine[j];
                }
            }

            initialUniverse = _initialUniverse;

            List<(int, int)> listOfGalaxies = new List<(int, int)>();

            for (int i = 0; i < initialUniverse.GetLength(0); i++)
            {
                for (int j = 0; j < initialUniverse.GetLength(1); j++)
                {
                    if (_initialUniverse[i, j] == '#')
                    {
                        listOfGalaxies.Add((i, j));
                    }
                }
            }

            long sumOfDistances = 0;

            for (int i = 0; i < listOfGalaxies.Count; i++)
            {
                for (int j = i + 1; j < listOfGalaxies.Count; j++)
                {
                    sumOfDistances += getDistance(listOfGalaxies[i], listOfGalaxies[j]);
                }
            }

            Console.WriteLine(sumOfDistances);

        }


        static int getDistance((int, int) galaxy1, (int, int) galaxy2)
        {
            int numberOfExpansions = 0;
            int minX = Math.Min(galaxy1.Item1, galaxy2.Item1);
            int maxX = Math.Max(galaxy1.Item1, galaxy2.Item1);
            int minY = Math.Min(galaxy1.Item2, galaxy2.Item2);
            int maxY = Math.Max(galaxy1.Item2, galaxy2.Item2);
            int rows = initialUniverse.GetLength(0);
            int columns = initialUniverse.GetLength(1);

            for (int i = minX; i < maxX; i++)
            {
                bool isEmpty = true;
                for(int j = 0; j < columns; j++)
                {
                    if (initialUniverse[i, j] == '#')
                    {
                        isEmpty = false;
                    }
                }
                if (isEmpty)
                {
                    numberOfExpansions++;
                }
            }

            for (int i = minY; i < maxY; i++)
            {
                bool isEmpty = true;
                for (int j = 0; j < rows; j++)
                {
                    if (initialUniverse[j, i] == '#')
                    {
                        isEmpty = false;
                    }
                }
                if (isEmpty)
                {
                    numberOfExpansions++;
                }
            }

            return Math.Abs(galaxy2.Item1 - galaxy1.Item1) + Math.Abs(galaxy2.Item2 - galaxy1.Item2) + numberOfExpansions * 999_999;
        }
    }
}
