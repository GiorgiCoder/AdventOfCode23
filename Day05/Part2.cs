
// ------------------------------------------------------------------  W A R N I N G! ----------------------------------------------------------------- \\
// The code itself is correct and is tested on smaller inputs, but it takes too much space to create arrays of size of millions or (~2) billions ------ \\
// I should've written it differently, maybe I will, but for now, ---  DO NOT RUN IT!  --- Otherwise - BOOM! ------------------------------------------ \\

namespace AdventOfCode.AdventOfCode2023.Day05
{
    internal class Part2
    {
        public static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day05\\input.txt";
            StreamReader reader = new StreamReader(filePath);
            string input = reader.ReadToEnd();

            string[] blocks = input.ReplaceLineEndings("\n").Split("\n\n", StringSplitOptions.TrimEntries);

            long[] seeds = blocks[0].Split(": ")[1].Split(' ').Select(s => long.Parse(s)).ToArray();
            List<(long, long)> rangesOfSeeds = new List<(long, long)>();

            for (int i = 0; i < seeds.Length; i += 2)
            {
                rangesOfSeeds.Add((seeds[i], seeds[i + 1]));

            }

            List<long> minLocations = new();

            foreach (var range in rangesOfSeeds)
            {
                long[] allSeeds = new long[range.Item2];
                for (long i = range.Item1; i < range.Item1 + range.Item2; i++)
                {
                    allSeeds[i - range.Item1] = i;
                }
                long l = finalResult(blocks, allSeeds);
                minLocations.Add(l);
            }

            Console.WriteLine(minLocations.Min());
        }

        public static long finalResult(string[] blocks, long[] array)
        {
            var soils = mapper(array, getData(blocks, 1));
            var fertilizers = mapper(soils, getData(blocks, 2));
            var waters = mapper(fertilizers, getData(blocks, 3));
            var lights = mapper(waters, getData(blocks, 4));
            var temperatures = mapper(lights, getData(blocks, 5));
            var humidities = mapper(temperatures, getData(blocks, 6));
            var locations = mapper(humidities, getData(blocks, 7));

            long minLocation = locations[0];

            for (int i = 0; i < locations.Length; i++)
            {
                if (locations[i] < minLocation)
                {
                    minLocation = locations[i];
                }
            }

            return minLocation;
        }

        static Dictionary<long, (long, long)> getData(string[] blocks, int m) // Dictionary <range, (sourceStart, destinationStart)>
        {
            Dictionary<long, (long, long)> result = new();

            string allData = blocks[m].Split("map:\n")[1];
            string[] dataLines = allData.Split("\n");

            foreach (string line in dataLines)
            {
                string[] parts = line.Split(" ");
                result.Add(long.Parse(parts[2]), (long.Parse(parts[1]), long.Parse(parts[0])));
            }

            return result;
        }

        static long[] mapper(long[] input, Dictionary<long, (long, long)> dict)
        {
            long[] mappedY = new long[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                mappedY[i] = input[i];
            }

            for (int i = 0; i < input.Length; i++)
            {
                foreach (var d in dict)
                {
                    if (isBetween(input[i], d.Value.Item1, d.Value.Item1 + d.Key))
                    {
                        mappedY[i] = input[i] + (d.Value.Item2 - d.Value.Item1);
                        break;
                    }
                }
            }
            return mappedY;
        }

        public static bool isBetween(long a, long b, long c)
        {
            bool result = (a >= b) && (a <= c) || (a <= b) && (a >= c);
            return result;
        }
    }
}

