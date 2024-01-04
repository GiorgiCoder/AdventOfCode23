// Maximized readibility

namespace AdventOfCode.AdventOfCode2023.Day24
{
    internal class Part1
    {
        public static int count = 0;
        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day24\\input.txt";
            StreamReader reader = new StreamReader(filePath);

            Dictionary<(long, long), (int, int)> coordinatesAndSlope = new Dictionary<(long, long), (int, int)>();
            string[] allPoints = reader.ReadToEnd().Split("\r\n");
            foreach (var point in allPoints)
            {
                string[] pointsAndMoves = point.Split("@");
                string[] points = pointsAndMoves[0].Split(",");
                string[] moves = pointsAndMoves[1].Split(",");
                // turns out int.Parse allows spaces, so we don't need to trim them
                long x = long.Parse(points[0]);
                long y = long.Parse(points[1]);
                int m = int.Parse(moves[0]);
                int n = int.Parse(moves[1]);

                coordinatesAndSlope.Add((x, y), (m, n));
            }

            for(int i = 0; i < coordinatesAndSlope.Count; i++)
            {
                for(int j = i + 1; j < coordinatesAndSlope.Count; j++)
                {
                    intersectionPoint((coordinatesAndSlope.ElementAt(i).Key, coordinatesAndSlope.ElementAt(i).Value),
                                       (coordinatesAndSlope.ElementAt(j).Key, coordinatesAndSlope.ElementAt(j).Value));
                }
            }

            Console.WriteLine(count); // 25261
        }

        public static void intersectionPoint(((long, long), (int, int)) point1, ((long, long), (int, int)) point2)
        {
            var ((x1, y1), (m1, n1)) = point1;
            var ((x2, y2), (m2, n2)) = point2;

            double k1 = (double)n1 / m1;
            double k2 = (double)n2 / m2;

            if (k1 == k2) { return; } // parallel
            
            // Writing y-y0 = k(x-x0) for both equations, we make them equal to find intersection point (x, y) and are left with this equation:
            double intersectX = (k1 * x1 - k2 * x2 - y1 + y2) / (k1 - k2);
            double intersectY = k1 * (intersectX - x1) + y1;

            if (intersectX <= 200000000000000 || intersectX >= 400000000000000 || intersectY <= 200000000000000 || intersectY >= 400000000000000) { return; }

            if ((intersectX - x1) / m1 > 0 && (intersectX - x2) / m2 > 0)
            {
                count++;
            }
        }

    }
}
