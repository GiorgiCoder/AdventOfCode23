using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2023.Day16
{
    internal class Part2
    {
        public static List<(int, int, int)> splitters = new List<(int, int, int)>(); // coordinate and direction
        public static Queue<(int, int, int)> splittersToDealWith = new Queue<(int, int, int)>();
        public static List<(int, int)> heatedPoints = new List<(int, int)>();
        public static char[,] puzzle;
        public static int totalHeat = 0;
        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day16\\input.txt";
            StreamReader reader = new StreamReader(filePath);
            string[] lines = reader.ReadToEnd().Split("\r\n");
            int rows = lines.Length;
            int cols = lines[0].Length;
            char[,] _puzzle = new char[rows + 2, cols + 2];

            // Filling the borders with .
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < cols + 2; j++)
                {
                    _puzzle[i, j] = '#';
                }
            }
            for (int i = rows + 1; i < rows + 2; i++)
            {
                for (int j = 0; j < cols + 2; j++)
                {
                    _puzzle[i, j] = '#';
                }
            }
            for (int i = 0; i < rows + 2; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    _puzzle[i, j] = '#';
                }
            }
            for (int i = 0; i < rows + 2; i++)
            {
                for (int j = cols + 1; j < cols + 2; j++)
                {
                    _puzzle[i, j] = '#';
                }
            }

            // Copying the original array
            for (int i = 1; i < rows + 1; i++)
            {
                char[] charsOfLine = lines[i - 1].ToCharArray();
                for (int j = 1; j < cols + 1; j++)
                {
                    _puzzle[i, j] = charsOfLine[j - 1];
                }
            }

            puzzle = _puzzle;

            int maxHeat = 0;

            void ProcessBorder(int x, int y, int direction)
            {
                splitters.Clear();
                splittersToDealWith.Clear();
                heatedPoints.Clear();
                totalHeat = 0;

                Move((x, y), direction);

                while (splittersToDealWith.Count != 0)
                {
                    var (newX, newY, dir) = splittersToDealWith.Peek();
                    Move((newX, newY), dir);
                    splittersToDealWith.Dequeue();
                }

                if (totalHeat > maxHeat)
                {
                    maxHeat = totalHeat;
                }
            }

            for (int i = 1; i <= puzzle.GetLength(0) - 2; i += puzzle.GetLength(0) - 3)
            {
                for (int j = 1; j <= puzzle.GetLength(1) - 2; j++)
                {
                    if (i == 1)
                    {
                        ProcessBorder(i, j, 4);
                    }
                    else
                    {
                        ProcessBorder(i, j, 2);
                    }
                }
            }

            for (int j = 1; j <= puzzle.GetLength(1) - 2; j += puzzle.GetLength(1) - 3)
            {
                for (int i = 1; i <= puzzle.GetLength(0) - 2; i++)
                {
                    if (j == 1)
                    {
                        ProcessBorder(i, j, 1);
                    }
                    else
                    {
                        ProcessBorder(i, j, 3);
                    }
                }
            }

            Console.WriteLine(maxHeat);
        }

        public static void Move((int, int) coordinate, int direction) // direction which it comes from: 1 = left, 2 = down, 3 = right, 4 = up
        {
            int x = coordinate.Item1;
            int y = coordinate.Item2;

            while (true)
            {
                if (splitters.Contains((x, y, direction)) || puzzle[x, y] == '#')
                {
                    break;
                }
                else
                {
                    splitters.Add((x, y, direction));
                    if (!heatedPoints.Contains((x, y)))
                    {
                        totalHeat++;
                        heatedPoints.Add((x, y));
                    }


                    switch (puzzle[x, y])
                    {
                        case '.':
                            switch (direction)
                            {
                                case 1: y++; break;
                                case 2: x--; break;
                                case 3: y--; break;
                                case 4: x++; break;
                            }
                            break;
                        case '/':
                            switch (direction)
                            {
                                case 1: x--; direction = 2; break;
                                case 2: y++; direction = 1; break;
                                case 3: x++; direction = 4; break;
                                case 4: y--; direction = 3; break;
                            }
                            break;
                        case '\\':
                            switch (direction)
                            {
                                case 1: x++; direction = 4; break;
                                case 2: y--; direction = 3; break;
                                case 3: x--; direction = 2; break;
                                case 4: y++; direction = 1; break;
                            }
                            break;
                        case '|':
                            if (direction == 1 || direction == 3)
                            {
                                splittersToDealWith.Enqueue((x - 1, y, 2));
                                x++;
                                direction = 4;
                            }
                            else if (direction == 2)
                            {
                                x--;
                            }
                            else // if direction == 4)
                            {
                                x++;
                            }
                            break;
                        case '-':
                            if (direction == 2 || direction == 4)
                            {
                                splittersToDealWith.Enqueue((x, y - 1, 3));
                                y++;
                                direction = 1;
                            }
                            else if (direction == 1)
                            {
                                y++;
                            }
                            else // if direction == 3)
                            {
                                y--;
                            }
                            break;
                    }
                }
            }
        }
    }
}

