namespace AdventOfCode.AdventOfCode2023.Day03
{
    internal class Part1
    {
        public static void main(string[] args)
        {
            string filePath = "C:\\Users\\Giorgi\\source\\repos\\AdventOfCode\\AdventOfCode\\AdventOfCode2023\\Day03\\input.txt";

            string[] lines = File.ReadAllLines(filePath);

            int rows = lines.Length;
            int columns = lines[0].Length;

            char[,] charArray = new char[rows+4, columns+4];
            bool[,] isVisited = new bool[rows+4, columns+4];

            

            for (int i = 0; i < 2; i++) // filling first two rows with periods (.)
            {
                for (int j = 0; j < columns + 4; j++)
                {
                    charArray[i, j] = '.';
                }
            }

            for (int i = 0; i < rows + 4; i++) // filling first two columns with periods (.)
            {
                for (int j = 0; j < 2; j++)
                {
                    charArray[i, j] = '.';
                }
            }

            for (int i = rows; i < rows + 4; i++) // filling last two rows with periods (.)
            {
                for (int j = 0; j < columns + 4; j++)
                {
                    charArray[i, j] = '.';
                }
            }

            for (int i = 0; i < rows ; i++) // filling last two columns with periods (.)
            {
                for (int j = columns; j < columns + 4; j++)
                {
                    charArray[i, j] = '.';
                }
            }

            // Copy the original content
            for (int i = 0; i < rows; i++)
            {
                char[] charsOfLine = lines[i].ToCharArray();
                for (int j = 0; j < columns; j++)
                {
                    charArray[i + 2, j + 2] = charsOfLine[j];
                }
            }

            List<int> resultsList = new List<int>();

            for(int i = 2; i < rows+2; i++)
            {
                for (int j = 2; j < columns+2; j++)
                {
                    if (!char.IsNumber(charArray[i, j]) && charArray[i,j] != '.')
                    {
                        if(char.IsDigit(charArray[i - 1, j - 1]) && !isVisited[i - 1, j - 1])
                        {
                            Tuple<bool[], int> n = getNumber(charArray[i - 1, j - 3], charArray[i - 1, j - 2],
                                charArray[i - 1, j - 1], charArray[i - 1, j], charArray[i - 1, j + 1]);
                            resultsList.Add(n.Item2);
                            if (n.Item1[0])
                                isVisited[i - 1, j - 3] = true;
                            if (n.Item1[1])
                                isVisited[i - 1, j - 2] = true;
                            if (n.Item1[2])
                                isVisited[i - 1, j - 1] = true;
                            if (n.Item1[3])
                                isVisited[i - 1, j    ] = true;
                            if (n.Item1[4])
                                isVisited[i - 1, j + 1] = true;
                        }
                        if (char.IsDigit(charArray[i - 1, j]) && !isVisited[i - 1, j])
                        {
                            Tuple<bool[], int> n = getNumber(charArray[i - 1, j - 2], charArray[i - 1, j - 1],
                                charArray[i - 1, j], charArray[i - 1, j + 1], charArray[i - 1, j + 2]);
                            resultsList.Add(n.Item2);
                            if (n.Item1[0])
                                isVisited[i - 1, j - 2] = true;
                            if (n.Item1[1])
                                isVisited[i - 1, j - 1] = true;
                            if (n.Item1[2])
                                isVisited[i - 1, j    ] = true;
                            if (n.Item1[3])
                                isVisited[i - 1, j + 1] = true;
                            if (n.Item1[4])
                                isVisited[i - 1, j + 2] = true;
                        }
                        if (char.IsDigit(charArray[i - 1, j + 1]) && !isVisited[i - 1, j + 1])
                        {
                            Tuple<bool[], int> n = getNumber(charArray[i - 1, j - 1], charArray[i - 1, j],
                                charArray[i - 1, j + 1], charArray[i - 1, j + 2], charArray[i - 1, j + 3]);
                            resultsList.Add(n.Item2);
                            if (n.Item1[0])
                                isVisited[i - 1, j - 1] = true;
                            if (n.Item1[1])
                                isVisited[i - 1, j    ] = true;
                            if (n.Item1[2])
                                isVisited[i - 1, j + 1] = true;
                            if (n.Item1[3])
                                isVisited[i - 1, j + 2] = true;
                            if (n.Item1[4])
                                isVisited[i - 1, j + 3] = true;

                        }
                        if (char.IsDigit(charArray[i, j - 1]) && !isVisited[i, j - 1])
                        {
                            Tuple<bool[], int> n = getNumber(charArray[i, j - 3], charArray[i, j - 2],
                                charArray[i, j - 1], charArray[i, j], charArray[i, j + 1]);
                            resultsList.Add(n.Item2);
                            if (n.Item1[0])
                                isVisited[i, j - 3] = true;
                            if (n.Item1[1])
                                isVisited[i, j - 2] = true;
                            if (n.Item1[2])
                                isVisited[i, j - 1] = true;
                            if (n.Item1[3])
                                isVisited[i, j] = true;
                            if (n.Item1[4])
                                isVisited[i, j + 1] = true;

                        }
                        if (char.IsDigit(charArray[i, j + 1]) && !isVisited[i, j + 1])
                        {
                            Tuple<bool[], int> n = getNumber(charArray[i, j - 1], charArray[i, j],
                                charArray[i, j + 1], charArray[i, j + 2], charArray[i, j + 3]);
                            resultsList.Add(n.Item2);
                            if (n.Item1[0])
                                isVisited[i, j - 1] = true;
                            if (n.Item1[1])
                                isVisited[i, j    ] = true;
                            if (n.Item1[2])
                                isVisited[i, j + 1] = true;
                            if (n.Item1[3])
                                isVisited[i, j + 2] = true;
                            if (n.Item1[4])
                                isVisited[i, j + 3] = true;
                        }
                        if (char.IsDigit(charArray[i + 1, j - 1]) && !isVisited[i + 1, j - 1])
                        {
                            Tuple<bool[], int> n = getNumber(charArray[i + 1, j - 3], charArray[i + 1, j - 2],
                                charArray[i + 1, j - 1], charArray[i + 1, j], charArray[i + 1, j + 1]);
                            resultsList.Add(n.Item2);
                            if (n.Item1[0])
                                isVisited[i + 1, j - 3] = true;
                            if (n.Item1[1])
                                isVisited[i + 1, j - 2] = true;
                            if (n.Item1[2])
                                isVisited[i + 1, j - 1] = true;
                            if (n.Item1[3])
                                isVisited[i + 1, j] = true;
                            if (n.Item1[4])
                                isVisited[i + 1, j + 1] = true;

                        }
                        if (char.IsDigit(charArray[i + 1, j]) && !isVisited[i + 1, j])
                        {
                            Tuple<bool[], int> n = getNumber(charArray[i + 1, j - 2], charArray[i + 1, j - 1],
                                charArray[i + 1, j], charArray[i + 1, j + 1], charArray[i + 1, j + 2]);
                            resultsList.Add(n.Item2);
                            if (n.Item1[0])
                                isVisited[i + 1, j - 2] = true;
                            if (n.Item1[1])
                                isVisited[i + 1, j - 1] = true;
                            if (n.Item1[2])
                                isVisited[i + 1, j    ] = true;
                            if (n.Item1[3])
                                isVisited[i + 1, j + 1] = true;
                            if (n.Item1[4])
                                isVisited[i + 1, j + 2] = true;

                        }
                        if (char.IsDigit(charArray[i + 1, j + 1]) && !isVisited[i + 1, j + 1])
                        {
                            Tuple<bool[], int> n = getNumber(charArray[i + 1, j - 1], charArray[i + 1, j],
                                charArray[i + 1, j + 1], charArray[i + 1, j + 2], charArray[i + 1, j + 3]);
                            resultsList.Add(n.Item2);
                            if (n.Item1[0])
                                isVisited[i + 1, j - 1] = true;
                            if (n.Item1[1])
                                isVisited[i + 1, j    ] = true;
                            if (n.Item1[2])
                                isVisited[i + 1, j + 1] = true;
                            if (n.Item1[3])
                                isVisited[i + 1, j + 2] = true;
                            if (n.Item1[4])
                                isVisited[i + 1, j + 3] = true;
                        }
                    }
                }
            }

            Console.WriteLine(resultsList.Sum());

        }

        public static Tuple<bool[], int> getNumber(char a, char b, char c, char d, char e)
        {
            if (char.IsDigit(a) && char.IsDigit(b) && char.IsDigit(c))
            {
                return Tuple.Create(new bool[] { true, true, true, false, false }, int.Parse($"{a}{b}{c}"));
            }
            else if (char.IsDigit(b) && char.IsDigit(c) && char.IsDigit(d))
            {
                return Tuple.Create(new bool[] { false, true, true, true, false }, int.Parse($"{b}{c}{d}"));
            }
            else if (char.IsDigit(c) && char.IsDigit(d) && char.IsDigit(e))
            {
                return Tuple.Create(new bool[] { false, false, true, true, true }, int.Parse($"{c}{d}{e}"));
            }
            else if (char.IsDigit(b) && char.IsDigit(c))
            {
                return Tuple.Create(new bool[] { false, true, true, false, false }, int.Parse($"{b}{c}"));
            }
            else if (char.IsDigit(c) && char.IsDigit(d))
            {
                return Tuple.Create(new bool[] { false, false, true, true, false }, int.Parse($"{c}{d}"));
            }
            else
            {
                return Tuple.Create(new bool[] { false, false, true, false, false }, int.Parse($"{c}"));
            }
        }

    }
}
