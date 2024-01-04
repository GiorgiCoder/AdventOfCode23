using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2023.Day14
{
    internal class Part1
    {
        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day14\\input.txt";
            StreamReader reader = new StreamReader(filePath);

            string[] allLines = reader.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            int rows = allLines.Length;
            int columns = allLines[0].Length;

            char[,] map = new char[rows + 1, columns];

            // Filling the upper border with #
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    map[i, j] = '#';
                }
            }

            // Copying the original array
            for (int i = 1; i < rows + 1; i++)
            {
                char[] charsOfLine = allLines[i - 1].ToCharArray();
                for (int j = 0; j < columns; j++)
                {
                    map[i, j] = charsOfLine[j];
                }
            }

            for (int i = 1; i < rows + 1; i++)
            {
                for (int k = i; k >= 1; k--)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (map[k, j] == 'O' && map[k - 1, j] == '.')
                        {
                            map[k, j] = '.';
                            map[k - 1, j] = 'O';
                        }
                    }
                }
            }

            int result = 0;

            for(int i = 1; i < rows + 1; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    if (map[i, j] == 'O')
                    {
                        result += rows + 1 - i;
                    }
                }
            }

            Console.WriteLine(result);
            
        }
    }
}
