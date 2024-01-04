namespace AdventOfCode.AdventOfCode2023.Day02
{
    internal class Part1
    {

        public static Dictionary<string, int> maxCapacity = new Dictionary<string, int>()
        {
            {"red", 12},
            {"green", 13},
            {"blue", 14}
        };

        public static void main(String[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day02\\input.txt";
            int countOfIDs = 0;
            StreamReader reader = new StreamReader(filePath);

            string line;
            while((line = reader.ReadLine()) != null)
            {
                countOfIDs += check(line);
            }
            Console.WriteLine(countOfIDs);
        }

        public static int check(string line)
        {
            bool isValid = true;
            string[] data = line.Split(": ");
            int Id = int.Parse(data[0].Split(" ")[1]);

            string[] sets = data[1].Split("; ");

            foreach (string s in sets)
            {
                string[] cubes = s.Split(", ");
                foreach (string c in cubes)
                {
                    string[] numAndColor = c.Split(" ");
                    int num = int.Parse(numAndColor[0]);
                    string color = numAndColor[1];
                    if (num > maxCapacity.FirstOrDefault(x => x.Key.Equals(color)).Value)
                    {
                        isValid = false;
                    }
                }
            }

            if (isValid)
                return Id;
            else
                return 0;
        }
    }
}
