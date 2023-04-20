namespace _02._Rocket
{
    using System;
    public class StartUp
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            int width = n + 5;

            for (int i = 0; i < width; i++)
            {
                if (i == width / 2) Console.Write('^');
                else Console.Write('_');
            }

            Console.WriteLine();

            int count = width / 2 - 1;
            int count2 = 1;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    Console.Write('_');
                }


                Console.Write('/');

                for (int j = 0; j < count2; j++)
                {
                    Console.Write('|');
                }

                count2 += 2;
                Console.Write('\\');

                for (int j = 0; j < count; j++)
                {
                    Console.Write('_');
                }
                count--;
                Console.WriteLine();
            }

            int x = n / 2 + 1;
            int dotCount = 1;

            for (int i = 0; i < x; i++)
            {
                if (i == x - 1)
                {
                    // _/.|||.\_
                    int dots = (width - 7) / 2;
                    Console.Write("_/");

                    for (int j = 0; j < dots; j++)
                    {
                        Console.Write('.');
                    }

                    Console.Write("|||");

                    for (int j = 0; j < dots; j++)
                    {
                        Console.Write('.');
                    }

                    Console.Write("\\_");
                    Console.WriteLine();
                }
                else
                {
                    for (int j = 0; j < count; j++)
                    {
                        Console.Write('_');
                    }

                    Console.Write('/');

                    for (int j = 0; j < dotCount; j++)
                    {
                        Console.Write('.');
                    }

                    Console.Write("|||");

                    for (int j = 0; j < dotCount; j++)
                    {
                        Console.Write('.');
                    }

                    Console.Write('\\');
                    for (int j = 0; j < count; j++)
                    {
                        Console.Write('_');
                    }

                    count--;
                    dotCount++;
                    Console.WriteLine();
                }
            }

            int c = (width - 3) / 2;
            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < c; j++)
                {
                    Console.Write('_');
                }

                Console.Write("|||");

                for (int j = 0; j < c; j++)
                {
                    Console.Write('_');
                }

                Console.WriteLine();
            }

            for (int i = 0; i < c; i++)
            {
                Console.Write('_');
            }

            Console.Write("~~~");

            for (int i = 0; i < c; i++)
            {
                Console.Write('_');
            }

            Console.WriteLine();

            int rowCount = n / 2;

            int d = 0;
            c--;
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    Console.Write('_');
                }

                Console.Write("//");

                for (int j = 0; j < d; j++)
                {
                    Console.Write('.');
                }

                Console.Write('!');

                for (int j = 0; j < d; j++)
                {
                    Console.Write('.');
                }

                Console.Write("\\\\");

                for (int j = 0; j < c; j++)
                {
                    Console.Write('_');
                }

                d++;
                c--;
                Console.WriteLine();
            }
        }
    }
}
