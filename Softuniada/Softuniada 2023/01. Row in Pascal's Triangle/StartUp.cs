namespace _01._Row_in_Pascal_s_Triangle
{
    using System;
    public class StartUp
    {
        static void Main()
        {
            int lines;
            long[][] pascalTriangle;
            GetInfo(out lines, out pascalTriangle);
            Engine(lines, pascalTriangle);
            IO(pascalTriangle);
        }

        private static void GetInfo(out int lines, out long[][] pascalTriangle)
        {
            lines = int.Parse(Console.ReadLine());
            pascalTriangle = new long[lines + 1][];
        }

        private static void Engine(int lines, long[][] pascalTriangle)
        {
            for (var row = 0; row <= lines; row++)
            {
                pascalTriangle[row] = new long[row + 1];
                pascalTriangle[row][0] = 1;
                pascalTriangle[row][^1] = 1;
                for (var col = 1; col < row; col++)
                {
                    pascalTriangle[row][col] = pascalTriangle[row - 1][col - 1] + pascalTriangle[row - 1][col];
                }
            }
        }

        private static void IO(long[][] pascalTriangle)
        {
            for (var row = pascalTriangle.GetLength(0) - 1; row > 0;)
            {
                Console.Write(string.Join(" ", pascalTriangle[row]));
                return;
            }
        }
    }
}
