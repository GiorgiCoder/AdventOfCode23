namespace AdventOfCode.AdventOfCode2023.Day19
{
    internal class Part1
    {
        // They key is ID & char (x, m, a, s). Value is value.
        public static Dictionary<int, (int, int, int, int)> xmasValues;
        // The key is workflow string. Value is char (x, m, a, s) & char (>, <) & int & nextWorkflow
        public static Dictionary<string, List<(char, char, int, string)>> workflowInfo;
        public static List<int> acceptedParts = new List<int>();
        public static List<int> rejectedParts = new List<int>();

        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day19\\input.txt";

            xmasValues = parseParts(filePath);
            workflowInfo = parseWorkflows(filePath);

            
            for(int i = 0; i < xmasValues.Count; i++)
            {
                iterator(xmasValues[i], "in");
            }

            Console.WriteLine(acceptedParts.Sum());

        }

        public static void iterator((int, int, int, int) charValue, string workFlow)
        {
            var (int1, int2, int3, int4) = charValue;

            if (workFlow == "A")
            {
                acceptedParts.Add(int1 + int2 + int3 + int4);
                return;
            }
            else if(workFlow == "R")
            {
                rejectedParts.Add(int1 + int2 + int3 + int4);
                return;
            }
            int num = workflowInfo[workFlow].Count;
            for (int i = 0; i < num; i++)
            {
                if (i == num - 1)
                {
                    iterator(charValue, workflowInfo[workFlow].ElementAt(i).Item4);
                    return;
                }
                else
                {
                    char charToCheck = workflowInfo[workFlow].ElementAt(i).Item1;
                    char sign = workflowInfo[workFlow].ElementAt(i).Item2;
                    int intToCompare = workflowInfo[workFlow].ElementAt(i).Item3;
                    string newWorkFlow = workflowInfo[workFlow].ElementAt(i).Item4;

                    int intOfPart = 0;

                    switch (charToCheck)
                    {
                        case 'x':
                            intOfPart = int1; break;
                        case 'm':
                            intOfPart = int2; break;
                        case 'a':
                            intOfPart = int3; break;
                        case 's':
                            intOfPart = int4; break;
                    }

                    bool satisfies = false;
                    switch (sign)
                    {
                        case '>':
                            satisfies = (intOfPart > intToCompare) ? true : false; break;
                        case '<':
                            satisfies = (intOfPart < intToCompare) ? true : false; break;
                    }

                    if (satisfies)
                    {
                        iterator(charValue, newWorkFlow);
                        return;
                    }
                }
            }
        }

        public static Dictionary<int, (int, int, int, int)> parseParts(string filePath)
        {
            Dictionary<int, (int, int, int, int)> _xmasValues = new Dictionary<int, (int, int, int, int)>();

            StreamReader reader = new StreamReader(filePath);
            string[] workflowsAndXmas = reader.ReadToEnd().Split("\r\n\r\n");
            string[] parts = workflowsAndXmas[1].Split("\r\n");

            for (int i = 0; i < parts.Length; i++)
            {
                string[] strings = parts[i].Split(',');
                strings[0] = strings[0].Substring(1);
                strings[3] = strings[3].Remove(strings[3].Length - 1);
                for(int j = 0; j < strings.Length; j++)
                {
                    strings[j] = strings[j].Split('=')[1];
                }
                _xmasValues.Add(i, (int.Parse(strings[0]), int.Parse(strings[1]), int.Parse(strings[2]), int.Parse(strings[3])));
            }

            return _xmasValues;
        }

        public static Dictionary<string, List<(char, char, int, string)>> parseWorkflows(string filePath)
        {
            Dictionary<string, List<(char, char, int, string)>> _workflowInfo = new Dictionary<string, List<(char, char, int, string)>>();
            StreamReader reader = new StreamReader(filePath);

            string[] workflowsAndXmas = reader.ReadToEnd().Split("\r\n\r\n");
            string[] workflows = workflowsAndXmas[0].Split("\r\n");

            for (int i = 0; i < workflows.Length; i++)
            {
                string[] tmp = workflows[i].Split('{');
                _workflowInfo.Add(tmp[0], new List<(char, char, int, string)>());
            }

            for (int i = 0; i < workflows.Length; i++)
            {
                string[] tmp = workflows[i].Split('{', '}');
                string workflow = tmp[0];
                string[] tmp1 = tmp[1].Split(',');
                for (int j = 0; j < tmp1.Length; j++)
                {
                    if (j != tmp1.Length - 1)
                    {
                        string[] tmp3 = tmp1[j].Split(':');
                        char v = tmp3[0].ElementAt(0);
                        int val = int.Parse(tmp3[0].Substring(2));
                        char sign = tmp3[0].ElementAt(1);
                        _workflowInfo[workflow].Add((v, sign, val, tmp3[1]));
                    }
                    else
                    {
                        _workflowInfo[workflow].Add(('0', '0', '0', tmp1[tmp1.Length - 1]));
                    }
                }
            }

            return _workflowInfo;
        }
    }
}
