namespace _03_
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class StartUp
    {
        static void Main()
        {
            Concert concert = new Concert();
            Operation(concert, Console.ReadLine());
            Console.WriteLine(concert.ToString());
        }
        private static Concert Operation(Concert concert, string inputLineFromConsole)
        {
            if (inputLineFromConsole == "Start!")
                return concert;
            var command = inputLineFromConsole.Split(new string[] { "; ", ", "}, StringSplitOptions.RemoveEmptyEntries).ToArray();
            switch (command.First())
            {
                case "Add":
                    {
                        var list = new List<string>();
                        foreach (var songs in command.Skip(2))
                            list.Add(songs);
                        concert.Add(list, command);
                    }
                    break;
                case "Play":
                    concert.Play(command);
                    break;
            }
            return Operation(concert, Console.ReadLine());
        }
    }
}
