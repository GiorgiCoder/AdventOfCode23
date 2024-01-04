using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.AdventOfCode2023.Day17
{
    internal class Part2
    {
        public static PriorityQueue<(int, int, int, int), int> priorityQueue = new PriorityQueue<(int, int, int, int), int>();
        public static Dictionary<(int, int), int>[][] Visited;
        public static int[][] map;

        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day17\\input.txt";
            string[] lines = File.ReadAllLines(filePath);

            int rows = lines.Length;
            int cols = lines[0].Length;

            Visited = new Dictionary<(int, int), int>[rows][];

            for (int i = 0; i < rows; i++)
            {
                Visited[i] = new Dictionary<(int, int), int>[cols];
                for (int x = 0; x < cols; x++)
                    Visited[i][x] = new Dictionary<(int, int), int>();
            }

            map = new int[lines.Length][];

            for (int y = 0; y < lines.Length; y++)
            {
                map[y] = new int[lines[y].Length];
                for (int x = 0; x < lines[y].Length; x++)
                    map[y][x] = lines[y][x] - '0';
            }

            priorityQueue.Enqueue((0, 0, 2, 0), 0);
            priorityQueue.Enqueue((0, 0, 3, 0), 0);

            while (priorityQueue.Count > 0)
            {
                var node = priorityQueue.Dequeue();
                int y = node.Item1;
                int x = node.Item2;
                int direction = node.Item3;
                int steps = node.Item4;

                int heat = 0;
                if (Visited[y][x].ContainsKey((direction, steps)))
                {
                    heat = Visited[y][x][(direction, steps)];
                }

                if (steps < 10)
                    Move(y, x, direction, heat, steps);

                if (steps >= 4)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int newDirection = direction;
                        if (i == 0)
                        {
                            newDirection += 1;
                        }
                        else
                        {
                            newDirection += 3;
                        }

                        if (newDirection > 4)
                        {
                            newDirection -= 4;
                        }

                        Move(y, x, newDirection, heat, 0);
                    }
                }
            }

            int maxX = rows - 1;
            int maxY = cols - 1;

            int minValue = Visited[maxX][maxY].Min(x => x.Value);

            Console.WriteLine(minValue);
        }

        private static void Move(int x, int y, int direction, int heat, int steps)
        {
            int directionX, directionY;

            if (direction == 1)
            {
                directionX = -1;
                directionY = 0;
            }
            else if (direction == 2)
            {
                directionX = 0;
                directionY = 1;
            }
            else if (direction == 3)
            {
                directionX = 1;
                directionY = 0;
            }
            else if (direction == 4)
            {
                directionX = 0;
                directionY = -1;
            }
            else
            {
                directionX = 0;
                directionY = 0;
            }

            for (int i = 1; i < 11; i++)
            {
                int newX = x + i * directionX;
                int newY = y + i * directionY;
                int newDirectionMoves = steps + i;

                if (newX < 0 || newX >= Visited.Length || newY < 0 || newY >= Visited[0].Length || newDirectionMoves > 10)
                    return;

                heat += map[newX][newY];

                if (i >= 4 && i <= 10)
                {
                    Dictionary<(int, int), int> visitedList = Visited[newX][newY];

                    if (!visitedList.ContainsKey((direction, newDirectionMoves)) || visitedList[(direction, newDirectionMoves)] > heat)
                    {
                        priorityQueue.Enqueue((newX, newY, direction, newDirectionMoves), heat);
                        visitedList[(direction, newDirectionMoves)] = heat;
                    }
                }
            }
        }
    }
}
