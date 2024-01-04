using System;
using System.IO;

namespace AdventOfCode.AdventOfCode2023.Day10
{
    internal class Part2
    {
        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day10\\input.txt";
            StreamReader reader = new StreamReader(filePath);

            string[] lines = reader.ReadToEnd().Split("\r\n");

            int rows = lines.Length;
            int columns = lines[0].Length;

            char[,] map = new char[rows + 2, columns + 2];
            bool[,] isVisited = new bool[rows + 2, columns + 2];

            // Filling the borders with .
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < columns + 2; j++)
                {
                    map[i, j] = '.';
                    isVisited[i, j] = true;
                }
            }
            for (int i = rows + 1; i < rows + 2; i++)
            {
                for (int j = 0; j < columns + 2; j++)
                {
                    map[i, j] = '.';
                    isVisited[i, j] = true;
                }
            }
            for (int i = 0; i < rows + 2; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    map[i, j] = '.';
                    isVisited[i, j] = true;
                }
            }
            for (int i = 0; i < rows + 2; i++)
            {
                for (int j = columns + 1; j < columns + 2; j++)
                {
                    map[i, j] = '.';
                    isVisited[i, j] = true;
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

            isVisited[rowOfS, colOfS] = false;

            int steps = 0;
            int x = 0, y = 0; // single pointer for starting from 1 point after S

            if (map[rowOfS - 1, colOfS] == '|' || map[rowOfS - 1, colOfS] == '7' || map[rowOfS - 1, colOfS] == 'F')
            {
                x = rowOfS - 1;
                y = colOfS;
            }
            else if (map[rowOfS, colOfS + 1] == '-' || map[rowOfS, colOfS + 1] == '7' || map[rowOfS, colOfS + 1] == 'J')
            {
                x = rowOfS;
                y = colOfS + 1;
            }
            else if (map[rowOfS + 1, colOfS] == '|' || map[rowOfS + 1, colOfS] == 'L' || map[rowOfS + 1, colOfS] == 'J')
            {
                x = rowOfS + 1;
                y = colOfS;
            }
            
            else if (map[rowOfS, colOfS - 1] == '-' || map[rowOfS, colOfS - 1] == 'L' || map[rowOfS, colOfS - 1] == 'F')
            {
                x = rowOfS;
                y = colOfS - 1;
            }

            List<(int, int)> allPoints = new List<(int, int)>();
            allPoints.Add((rowOfS, colOfS));

            steps = 1;

            while (x != rowOfS || y != colOfS)
            {
                isVisited[x, y] = true;
                allPoints.Add((x, y));

                switch (map[x, y])
                {

                    case '-':
                        y = !isVisited[x, y - 1] ? y - 1 : y + 1;
                        break;
                    case '|':
                        x = !isVisited[x - 1, y] ? x - 1 : x + 1;
                        break;
                    case 'L':
                        if (!isVisited[x - 1, y]) { x = x - 1; }
                        else { y = y + 1; }
                        break;
                    case 'J':
                        if (!isVisited[x - 1, y]) { x = x - 1; }
                        else { y = y - 1; }
                        break;
                    case '7':
                        if (!isVisited[x + 1, y]) { x = x + 1;  }
                        else { y = y - 1; }
                        break;
                    case 'F':
                        if (!isVisited[x, y + 1]) { y = y + 1; }
                        else { x = x + 1; }
                        break;
                }

                steps++;
            }

            allPoints.Add((rowOfS, colOfS));


            // I am writing this on day 18. I found out about shoelace formula to compute area of a polygon using its coordinates.
            // Then I use Pick's theorem (A = i + b/2 - 1) to compute how many points are inside the shape. Wasn't that hard at all :)

            int s1 = 0, s2 = 0;

            for (int i = 0; i < allPoints.Count - 1; i++)
            {
                s1 += allPoints.ElementAt(i).Item1 * allPoints.ElementAt(i + 1).Item2;
                s2 += allPoints.ElementAt(i).Item2 * allPoints.ElementAt(i + 1).Item1;
            }

            int area = Math.Abs(s1 - s2) / 2;

            int pointsInside = area - allPoints.Count / 2 + 1;

            Console.WriteLine(pointsInside);


        }
    }
}
