namespace _03._Longest_Valid_Parentheses
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        static void Main()
        {
            string inputFromConsole;
            Queue<char> brackets;
            int maxBracketsCount;
            GetInfo(out inputFromConsole, out brackets, out maxBracketsCount);
            maxBracketsCount = Engine(inputFromConsole, brackets, maxBracketsCount);
            IO(maxBracketsCount);
        }

        private static void GetInfo(out string inputFromConsole, out Queue<char> brackets, out int maxBracketsCount)
        {
            inputFromConsole = Console.ReadLine();
            brackets = new Queue<char>();
            maxBracketsCount = int.MinValue;
        }

        private static int Engine(string inputFromConsole, Queue<char> brackets, int maxBracketsCount)
        {
            for (int currentSymbol = 0; currentSymbol < inputFromConsole.Length - 1; currentSymbol++)
            {
                char firstSymbol = inputFromConsole[currentSymbol];
                char secondSymbol = inputFromConsole[currentSymbol + 1];
                if (firstSymbol == '(')
                {
                    if (secondSymbol == ')')
                    {
                        brackets.Enqueue(firstSymbol);
                        brackets.Enqueue(secondSymbol);
                        if (maxBracketsCount < brackets.Count)
                            maxBracketsCount = brackets.Count;
                    }
                    else
                    {
                        if (maxBracketsCount < brackets.Count)
                            maxBracketsCount = brackets.Count;
                        brackets.Clear();
                    }
                }
                else
                    continue;
            }

            return maxBracketsCount;
        }

        private static void IO(int maxBracketsCount)
        {
            Console.WriteLine(maxBracketsCount);
        }
    }
}
