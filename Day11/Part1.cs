using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2023.Day11
{
    internal class Part1
    {
        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day11\\input.txt";
            StreamReader reader = new StreamReader(filePath);

            string[] lines = reader.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            int rows = lines.Length;
            int cols = lines[0].Length;
            char[,] galaxyBefore = new char[rows, cols];

            List<int> rowsToAdd = new List<int>();
            List<int> colsToAdd = new List<int>();
            int count = 1;

            for (int i = 0; i < rows; i++)
            {
                char[] charsOfLine = lines[i].ToCharArray();
                
                for (int j = 0; j < cols; j++)
                {
                    galaxyBefore[i, j] = charsOfLine[j];
                }

                if (charsOfLine.All(c => c == '.'))
                {
                    rowsToAdd.Add(i + count);
                    count++;
                }
            }

            count = 1;

            for(int j = 0; j < cols; j++)
            {
                bool noGalaxies = true;
                for(int i = 0; i < rows; i++)
                {
                    if (galaxyBefore[i, j] == '#') { noGalaxies = false; break; }
                }
                if (noGalaxies) 
                { 
                    colsToAdd.Add(j + count); 
                    count++; 
                }
            }

            int newRows = rows + rowsToAdd.Count;
            int newCols = cols + colsToAdd.Count;
            char[,] galaxyAfter = new char[newRows, newCols];

            int rowIndex = 0;

            for(int i = 0; i < newRows; i++)
            {
                int colIndex = 0;
                if (rowsToAdd.Contains(i))
                {
                    for(int j = 0; j < newCols; j++)
                    {
                        galaxyAfter[i, j] = '.';
                    }
                }
                else
                {
                    for(int j = 0; j < newCols; j++)
                    {
                        if (colsToAdd.Contains(j))
                        {
                            galaxyAfter[i, j] = '.';
                        }
                        else
                        {
                            galaxyAfter[i, j] = galaxyBefore[rowIndex, colIndex++];
                        }
                    }
                    rowIndex++;
                }
            }

            List<(int, int)> listOfGalaxies = new List<(int, int)>();

            for(int i = 0; i < newRows; i++)
            {
                for(int j = 0; j < newCols; j++)
                {
                    if (galaxyAfter[i,j] == '#')
                    {
                        listOfGalaxies.Add((i, j));
                    }
                }
            }

            int sumOfDistances = 0;

            for(int i = 0; i < listOfGalaxies.Count; i++)
            {
                for(int j = i + 1;  j < listOfGalaxies.Count; j++)
                {
                    sumOfDistances += getDistance(listOfGalaxies[i], listOfGalaxies[j]);
                }
            }

            Console.WriteLine(sumOfDistances);
        }

        static int getDistance((int, int) galaxy1, (int, int) galaxy2)
        {
            return Math.Abs(galaxy2.Item1 - galaxy1.Item1) + Math.Abs(galaxy2.Item2 - galaxy1.Item2);
        }
    }
}
