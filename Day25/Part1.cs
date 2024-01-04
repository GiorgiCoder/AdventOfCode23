using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2023.Day25
{
    internal class Part1
    {
        public static Stack<string> group1 = new Stack<string>();
        public static Stack<string> group2 = new Stack<string>();
        public static Dictionary<string, List<string>> connections = new Dictionary<string, List<string>>();
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day25\\input.txt";
            StreamReader reader = new StreamReader(filePath);

            string line = reader.ReadToEnd();
            string[] lines = line.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> numberOfConnections = new Dictionary<string, int>();
            
            for(int i = 0; i < lines.Length; i++)
            {
                List<string> connectedTo = lines[i].Trim().Split(" ").ToList();
                connections.Add(lines[i], connectedTo);
                
            }
        }
    }
}
