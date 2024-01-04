using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2023.Day15
{
    internal class Part2
    {
        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day15\\input.txt";
            StreamReader reader = new StreamReader(filePath);

            string[] strings = reader.ReadToEnd().Split(',', StringSplitOptions.RemoveEmptyEntries);
            Dictionary<int, List<(string, int)>> dict = new Dictionary<int, List<(string, int)>>(); // list of boxIds, labels and focal lengths
            for(int i = 0; i < 256;  i++)
            {
                List<(string, int)> list = new List<(string, int)>();
                dict.Add(i, list);
            }

            for (int i = 0; i < strings.Length; i++)
            {
                var myTriple = getBoxNumber(strings[i]);
                int boxID = myTriple.Item1;
                string label = myTriple.Item2;
                int focalLength = myTriple.Item3;
                if(focalLength != 0) // when we have =
                {
                    if (dict[boxID].Any(x => x.Item1 == label)) // check if label is already in the box
                    {
                        for (int k = 0; k < dict[boxID].Count; k++)
                        {
                            if (dict[boxID].ElementAt(k).Item1 == label)
                            {
                                var tupleToUpdate = dict[boxID].ElementAt(k);
                                dict[boxID][k] = (tupleToUpdate.Item1, focalLength);
                            }
                        }
                    }
                    else // if was not present
                    {
                        dict[boxID].Add((label, focalLength));
                    }
                }
                else // when we have -
                {
                    if (dict[boxID].Any(x => x.Item1 == label)) // if label is present
                    {
                        var labelToDelete = dict[boxID].FirstOrDefault(x => x.Item1 == label);
                        dict[boxID].Remove(labelToDelete);
                    }
                    // if it wasn't present, we do nothing
                }
            }

            int sumOfResults = 0;

            for(int i = 0; i < 256;  i++)
            {
                int result = 0;
                for(int j = 0; j < dict[i].Count; j++)
                {
                    int boxIDPlus1 = i + 1;
                    int slotOfLabel = j + 1;
                    int focalLength = dict[i].ElementAt(j).Item2;
                    result = boxIDPlus1 * slotOfLabel * focalLength;
                    sumOfResults += result;
                }
            }

            Console.WriteLine(sumOfResults);

        }

        public static (int, string, int) getBoxNumber(string label) // returns label HASH value (box ID), label and its focal length (0 if -)
        {
            int result = 0;
            string updatedLabel;
            int focalLength = 0;
            if (label.Contains("="))
            {
                string[] s = label.Split('=');
                focalLength = int.Parse(s[1]);
                updatedLabel = s[0];
            }
            else
            {
                updatedLabel = label.Remove(label.Length - 1);
            }

            char[] chars = updatedLabel.ToCharArray();


            for (int j = 0; j < chars.Length; j++)
            {
                result += chars[j].GetHashCode();
                result *= 17;
                result = result % 256;
            }

            return (result, updatedLabel, focalLength);
        }
    }
}
