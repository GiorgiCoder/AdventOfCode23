using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2023.Day18
{
    internal class Part1
    {
        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day18\\input.txt";
            StreamReader reader = new StreamReader(filePath);

            string[] lines = reader.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            List<(char, int)> instructions = new List<(char, int)>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                instructions.Add((Convert.ToChar(line[0]), Convert.ToInt32(line[1])));
            }

            List<(int, int)> cubeHoles = new List<(int, int)>();

            int x = 0, y = 0;
            cubeHoles.Add((x, y));

            for(int i = 0; i < instructions.Count; i++)
            {
                var (direction, steps) = instructions.ElementAt(i);

                for (int j = 1; j <= steps; j++)
                {
                    if (direction == 'U' || direction == 'D')
                    {
                        cubeHoles.Add((direction == 'U' ? x - j : x + j, y));
                    }
                    else if (direction == 'R' || direction == 'L')
                    {
                        cubeHoles.Add((x, direction == 'R' ? y + j : y - j));
                    }
                }

                if (direction == 'U') { x -= steps; }
                else if (direction == 'D') { x += steps; }
                else if (direction == 'R') { y += steps; }
                else if (direction == 'L') { y -= steps; }
            }

            // Shoelace formula
            int s1 = 0, s2 = 0;

            for(int i = 0; i < cubeHoles.Count - 1; i++)
            {
                s1 += cubeHoles.ElementAt(i).Item1 * cubeHoles.ElementAt(i + 1).Item2;
                s2 += cubeHoles.ElementAt(i).Item2 * cubeHoles.ElementAt(i + 1).Item1;
            }

            int area = Math.Abs(s1 - s2) / 2;
            int totalCubes = area + cubeHoles.Count / 2 + 1;

            Console.Write(totalCubes);
        }
    }
}
