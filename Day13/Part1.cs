namespace AdventOfCode.AdventOfCode2023.Day13
{
    internal class Part1
    {
        public static int result;
        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day13\\input.txt";
            StreamReader reader = new StreamReader(filePath);
            string[] patterns = reader.ReadToEnd().Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);
            char[,] ground;
            int result = 0;

            for(int k = 0; k < patterns.Length; k++)
            {
                string[] rowLines = patterns[k].Split("\r\n");
                int rows = rowLines.Length;
                int cols = rowLines[0].Length;
                ground = new char[rows, cols];

                for(int i = 0; i < rowLines.Length; i++)
                {
                    char[] charsOfLine = rowLines[i].ToCharArray();
                    
                    for(int j = 0; j < rowLines[0].Length; j++)
                    {
                        ground[i, j] = charsOfLine[j];
                    }
                }

                result = findVerticalLine(ground);
            }


            Console.WriteLine(result);
        }

        public static int findVerticalLine(char[,] charArray)
        {
            return 0;
        }
    }
}
