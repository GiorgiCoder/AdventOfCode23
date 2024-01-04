using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2023.Day07
{
    internal class Part1
    {

        public static Dictionary<char, int> cardAndValue = new Dictionary<char, int>()
        {
            {'2', 2 }, {'3', 3 }, {'4', 4 }, {'5', 5 }, {'6', 6 }, {'7', 7 }, {'8', 8 },
            {'9', 9 }, {'T', 10 }, {'J', 11}, {'Q', 12 }, {'K', 13 }, {'A', 14}
        };

        static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day07\\input.txt";
            StreamReader reader = new StreamReader(filePath);
            string input = reader.ReadToEnd();

            string[] gamesAndBids = input.Split("\n");

            List<(string, string)> allHands = new();

            foreach(string s in gamesAndBids)
            {
                string[] spl = s.Split(" ");
                allHands.Add((spl[0], spl[1].Trim()));
            }


            var sortedArray = quickSort(allHands);
            long output = 0;
            int c = sortedArray.Count();

            for(int i = 0; i < sortedArray.Count; i++)
            {
                output += int.Parse(sortedArray[i].Item2) * (c - i);
            }

            Console.WriteLine(output);

        }




        // Made a dictionary of a character and number of that character in a hand.
        // Each combination has its own maxSize. These are (from the strongest combination to the weakest): 5, 4, 3, 3, 2, 2, 1
        // If maxSizes are equal like in cases 3&4 and 5&6, we compare secondMaxSizes, which are (for these four): 2, 1, 2, 1
        // If they are also equal, then the hands have the same power and we need to compare strings.
        static int compareHands(string _hand1, string _hand2) // returns 1 if hand1 > hand2 and -1 if hand1 < hand2
        {
            Dictionary<char, int> charsOfHand1 = new();
            Dictionary<char, int> charsOfHand2 = new();
            char[] hand1 = _hand1.ToCharArray();
            char[] hand2 = _hand2.ToCharArray();

            for (int i = 0; i < hand1.Length; i++)
            {
                if (charsOfHand1.ContainsKey(hand1[i]))
                {
                    charsOfHand1[hand1[i]]++;
                }
                else
                {
                    charsOfHand1.Add(hand1[i], 1);
                }

                if (charsOfHand2.ContainsKey(hand2[i]))
                {
                    charsOfHand2[hand2[i]]++;
                }
                else
                {
                    charsOfHand2.Add(hand2[i], 1);
                }
            }

            int maxSize1 = charsOfHand1.Max(x => x.Value);
            int maxSize2 = charsOfHand2.Max(x => x.Value);


            if (maxSize1 > maxSize2)
            {
                return 1;
            }
            else if (maxSize1 < maxSize2)
            {
                return -1;
            }
            else
            {
                int secondMaxSize1 = charsOfHand1.OrderByDescending(x => x.Value).Skip(1).FirstOrDefault().Value;
                int secondMaxSize2 = charsOfHand2.OrderByDescending(x => x.Value).Skip(1).FirstOrDefault().Value;
                if(secondMaxSize1 > secondMaxSize2)
                {
                    return 1;
                }
                else if (secondMaxSize2 > secondMaxSize1)
                {
                    return -1;
                }
            }

            // reaches this line if secondMaxSizes were equal too, meaning hands have the same combination, so we check for letters.

            int result = 1;
            
            for(int i = 0; i < hand1.Length; i++)
            {
                if (cardAndValue[hand1[i]] > cardAndValue[hand2[i]])
                {
                    result = 1;
                    break;
                }
                else if (cardAndValue[hand1[i]] < cardAndValue[hand2[i]])
                {
                    result = -1;
                    break;
                }
            }

            return result;

        }


        public static List<(string, string)> quickSort(List<(string, string)> hands)
        {
            if (hands.Count <= 1)
            {
                return hands;
            }

            Random random = new Random();
            int splitter = random.Next(hands.Count);

            int numbersLess = 0, numbersGreater = 0;

            for (int i = 0; i < hands.Count; i++)
            {
                if(i != splitter)
                {
                    if(compareHands(hands[i].Item1, hands[splitter].Item1) < 0)
                    {
                        numbersGreater++;
                    }
                    else { numbersLess++; }
                }
            }

            List<(string, string)> lessList = new List<(string, string)>(numbersLess);
            List<(string, string)> greaterList = new List<(string, string)>(numbersGreater);

            for (int i = 0; i < hands.Count; i++)
            {
                if (i != splitter)
                {
                    if(compareHands(hands[i].Item1, hands[splitter].Item1) < 0)
                    {
                        greaterList.Add(hands[i]);
                    }
                    else
                    {
                        lessList.Add(hands[i]);
                    }
                }
            }

            List<(string, string)> result = new List<(string, string)>(lessList.Count + greaterList.Count + 1);
            result.AddRange(quickSort(lessList));
            result.Add(hands[splitter]);
            result.AddRange(quickSort(greaterList));

            return result;
        }
    }
}
