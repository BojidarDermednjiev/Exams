namespace _05._Biggest_Formed_Number
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        static void Main(string[] args)
        {
            int[] readNumberFromConsole = GetInfo();
            StringBuilder sb = Engine(readNumberFromConsole);
            IO(sb);
        }
        private static int[] GetInfo()
            => Console.ReadLine().Split().Select(int.Parse).ToArray();
        private static StringBuilder Engine(int[] readNumberFromConsole)
        {
            Array.Sort(readNumberFromConsole);
            string[] dataForNumbers = new string[readNumberFromConsole.Length];
            StringBuilder sb = new();
            for (int i = 0; i < readNumberFromConsole.Length; i++)
                dataForNumbers[i] = readNumberFromConsole[i].ToString();
            Array.Sort(dataForNumbers, (a, b)
                => { return (b + a).CompareTo(a + b); });
            if (dataForNumbers[0] == "0")
                Console.WriteLine("0");
            foreach (var st in dataForNumbers)
                sb.Append(st);
            return sb;
        }
        private static void IO(StringBuilder sb)
        {
            Console.WriteLine(sb.ToString());
        }
    }
}
