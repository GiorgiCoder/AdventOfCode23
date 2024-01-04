using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.AdventOfCode2023.Day19
{
    internal class Part2
    {
        // The key is workflow string. Value is char (x, m, a, s) & char (>, <) & int & nextWorkflow
        public static Dictionary<string, List<(char, char, int, string)>> workflowInfo;

        public static Dictionary<int, (List<(int, int)>, List<(int, int)>, List<(int, int)>, List<(int, int)>)> ranges =
            new Dictionary<int, (List<(int, int)>, List<(int, int)>, List<(int, int)>, List<(int, int)>)>();

        public static List<(int, int)> allThatLeadToA = new List<(int, int)>();
        public static (List<(int, int)>, List<(int, int)>, List<(int, int)>, List<(int, int)>) allRanges =
            (new List<(int, int)> { }, new List<(int, int)> { }, new List<(int, int)> { }, new List<(int, int)> { });

        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day19\\input.txt";

            workflowInfo = parseWorkflows(filePath);


            for (int i = 0; i < workflowInfo.Count; i++)
            {
                iterate(workflowInfo.ElementAt(i).Key, new List<(int, int)> { }, new List<(int, int)> { }, new List<(int, int)> { }, new List<(int, int)> { });
            }

            long result = 1;

            int min = 0, max = 0;

            (min, max) = FindIntersection(allRanges.Item1);
            result *= (max - min);
            (min, max) = FindIntersection(allRanges.Item2);
            result *= (max - min);
            (min, max) = FindIntersection(allRanges.Item3);
            result *= (max - min);
            (min, max) = FindIntersection(allRanges.Item4);
            result *= (max - min);

            Console.Write(result);
        }



        public static void iterate(string s, List<(int, int)> xRanges, List<(int, int)> mRanges, List<(int, int)> aRanges, List<(int, int)> sRanges)
        {
            if (s == "A")
            {
                int minItem = 0;
                int maxItem = 0;
                if (xRanges.Count != 0)
                {
                    (minItem, maxItem) = FindIntersection(xRanges);
                    allRanges.Item1.Add((minItem, maxItem));
                }
                if (mRanges.Count != 0)
                {
                    (minItem, maxItem) = FindIntersection(mRanges);
                    allRanges.Item2.Add((minItem, maxItem));
                }
                if (aRanges.Count != 0)
                {
                    (minItem, maxItem) = FindIntersection(aRanges);
                    allRanges.Item3.Add((minItem, maxItem));
                }
                if (sRanges.Count != 0)
                {
                    (minItem, maxItem) = FindIntersection(sRanges);
                    allRanges.Item4.Add((minItem, maxItem));
                }
                return;
            } else if (s == "R") { return; }

            var newWfX = new List<(int, int)>();
            var newWfM = new List<(int, int)>();
            var newWfA = new List<(int, int)>();
            var newWfS = new List<(int, int)>();

            for (int i = 0; i < workflowInfo[s].Count; i++)
            {
                var (c, sign, intToCompare, newWorkFlow) = workflowInfo[s].ElementAt(i);

                switch (c)
                {
                    case 'x':
                        if(sign == '>')
                        {
                            xRanges.Add((intToCompare + 1, 4000));
                            newWfX.Add((0, intToCompare));
                        }
                        else
                        {
                            xRanges.Add((0, intToCompare - 1));
                            newWfX.Add((intToCompare, 4000));
                        }
                        continue;
                    case 'm':
                        if (sign == '>')
                        {
                            mRanges.Add((intToCompare + 1, 4000));
                            newWfM.Add((0, intToCompare));
                        }
                        else
                        {
                            mRanges.Add((0, intToCompare - 1));
                            newWfM.Add((intToCompare , 4000));
                        }
                        continue;
                    case 'a':
                        if (sign == '>')
                        {
                            aRanges.Add((intToCompare + 1, 4000));
                            newWfA.Add((0, intToCompare));
                        }
                        else
                        {
                            aRanges.Add((0, intToCompare - 1));
                            newWfA.Add((intToCompare, 4000));
                        }
                        continue;
                    case 's':
                        if (sign == '>')
                        {
                            sRanges.Add((intToCompare + 1, 4000));
                            newWfS.Add((0, intToCompare));
                        }
                        else
                        {
                            sRanges.Add((0, intToCompare - 1));
                            newWfS.Add((intToCompare, 4000));
                        }
                        continue;
                    default:
                        iterate(newWorkFlow, newWfX, newWfM, newWfA, newWfS);
                        break;
                }

                iterate(newWorkFlow, xRanges, mRanges, aRanges, sRanges);
                

            }
            
        }


        public static (int, int) FindIntersection(List<(int, int)> ranges)
        {

            int l = ranges[0].Item1;
            int r = ranges[0].Item2;

            for (int i = 1; i < ranges.Count; i++)
            {
                if (ranges[i].Item1 > r || ranges[i].Item2 < l)
                {
                    return (0, 1);
                }
                else
                {
                    l = Math.Max(l, ranges[i].Item1);
                    r = Math.Min(r, ranges[i].Item2);
                }
            }

            return (l, r);
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
