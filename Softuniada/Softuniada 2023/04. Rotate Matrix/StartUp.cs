namespace _04._Rotate_Matrix
{
    using System;
    using System.Linq;

    public class StartUp
    {
        static void Main()
        {
            int[,] matrix = GetInfo();
            FillMatrix(matrix);
            Engine(matrix);
            PrintMatrix(matrix);
        }
        private static int[,] GetInfo()
        {
            int readSizeOfMatrixFromConsole = int.Parse(Console.ReadLine());
            var matrix = new int[readSizeOfMatrixFromConsole, readSizeOfMatrixFromConsole];
            return matrix;
        }
        private static void FillMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                var fillMatrixWithNumbers = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                for (int col = 0; col < matrix.GetLength(1); col++)
                    matrix[row, col] = fillMatrixWithNumbers[col];
            }
        }
        private static void Engine(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0) / 2; row++)
                for (int col = 0; col < Math.Ceiling(((double)matrix.GetLength(1) / 2)); col++)
                {
                    var temp = matrix[row, col];
                    matrix[row, col] = matrix[matrix.GetLength(1) - 1 - col, row];
                    matrix[matrix.GetLength(1) - 1 - col, row] = matrix[matrix.GetLength(0) - 1 - row, matrix.GetLength(1) - 1 - col];
                    matrix[matrix.GetLength(0) - 1 - row, matrix.GetLength(1) - 1 - col] = matrix[col, matrix.GetLength(0) - 1 - row];
                    matrix[col, matrix.GetLength(0) - 1 - row] = temp;
                }
        }
        private static void PrintMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                    Console.Write(matrix[row, col] + " ");
                Console.WriteLine();
            }
        }
    }
}
