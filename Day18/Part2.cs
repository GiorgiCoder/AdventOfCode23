namespace AdventOfCode.AdventOfCode2023.Day18
{
    internal class Part2
    {
        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day18\\input.txt";
            StreamReader reader = new StreamReader(filePath);

            string[] lines = reader.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            List<(int, int)> instructions = new List<(int, int)>(); // 0 - R, 1 - D, 2 - L, 3 - U

            for (int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string hexValue = line[2].Substring(2, 5);

                int decValue = Convert.ToInt32(hexValue, 16);
                int direction = int.Parse(line[2].Substring(7, 1));

                instructions.Add((direction, decValue));
            }

            List<(long, long)> cubeHoles = new List<(long, long)>();

            int x = 0, y = 0;
            cubeHoles.Add((x, y));

            for (int i = 0; i < instructions.Count; i++)
            {
                var (direction, steps) = instructions.ElementAt(i);

                for (int j = 1; j <= steps; j++)
                {
                    if (direction == 3 || direction == 1)
                    {
                        cubeHoles.Add((direction == 3 ? x - j : x + j, y));
                    }
                    else if (direction == 0 || direction == 2)
                    {
                        cubeHoles.Add((x, direction == 0 ? y + j : y - j));
                    }
                }

                if (direction == 3) { x -= steps; }
                else if (direction == 1) { x += steps; }
                else if (direction == 0) { y += steps; }
                else if (direction == 2) { y -= steps; }
            }

            // Shoelace formula
            long s1 = 0, s2 = 0;

            for (int i = 0; i < cubeHoles.Count - 1; i++)
            {
                s1 += cubeHoles.ElementAt(i).Item1 * cubeHoles.ElementAt(i + 1).Item2;
                s2 += cubeHoles.ElementAt(i).Item2 * cubeHoles.ElementAt(i + 1).Item1;
            }

            long area = Math.Abs(s1 - s2) / 2;

            long totalCubes = area + cubeHoles.Count / 2 + 1;

            Console.WriteLine(totalCubes);
        }
    }
}