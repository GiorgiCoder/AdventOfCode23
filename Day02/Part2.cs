namespace AdventOfCode.AdventOfCode2023.Day02
{
    internal class Part2
    {

        public static void main(String[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day02\\input.txt";
            int sumOfPowers = 0;
            StreamReader reader = new StreamReader(filePath);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                sumOfPowers += power(line);
            }

            Console.WriteLine(sumOfPowers);
        }

        public static int power(string line)
        {
            string[] data = line.Split(": ");
            int Id = int.Parse(data[0].Split(" ")[1]);
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>()
                    {
                        {"red", 1 }, {"green", 1 }, {"blue", 1 }
                    };

            string[] sets = data[1].Split("; ");

            foreach (string s in sets)
            {
                string[] cubes = s.Split(", ");
                foreach (string c in cubes)
                {
                    string[] numAndColor = c.Split(" ");
                    int num = int.Parse(numAndColor[0]);
                    string color = numAndColor[1];
                    

                    if(num > keyValuePairs.FirstOrDefault(x => x.Key.Equals(color)).Value)
                    {
                        keyValuePairs[color] = num;
                    }
                }
            }

            return keyValuePairs["red"] * keyValuePairs["green"] * keyValuePairs["blue"];

        }
    }
}
