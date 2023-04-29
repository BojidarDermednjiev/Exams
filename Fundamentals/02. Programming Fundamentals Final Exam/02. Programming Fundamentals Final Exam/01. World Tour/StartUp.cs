namespace _01._World_Tour
{
    using System;
    using System.Text;

    public class StartUp
    {
        static void Main()
        {
            var destination = new StringBuilder(Console.ReadLine());
            string inputLineFromConsole;
            while ((inputLineFromConsole = Console.ReadLine()) != "Travel")
            {
                var command = inputLineFromConsole.Split(":", StringSplitOptions.RemoveEmptyEntries);
                switch (command[0])
                {
                    case "Add Stop":
                        Add(command, destination);
                        break;
                    case "Remove Stop":
                        Remove(command, destination);
                        break;
                    case "Switch":
                        Switch(command, destination);
                        break;
                }
                Console.WriteLine(destination);
            }
            Console.WriteLine($"Ready for world tour! Planned stops: {destination}");
        }

        private static void Add(string[] command, StringBuilder destination)
        {
            var index = int.Parse(command[1]);
            var newDestination = command[2];
            if (index >= 0 && index < destination.Length)
                destination.Insert(index, newDestination);
        }

        private static void Remove(string[] command, StringBuilder destination)
        {
            var startIndex = int.Parse(command[1]);
            var endIndex = int.Parse(command[2]);
            if (startIndex >= 0 && startIndex < destination.Length && endIndex >= 0 && endIndex < destination.Length)
                destination.Remove(startIndex, endIndex - startIndex + 1);
        }

        private static void Switch(string[] command, StringBuilder destination)
        {
            if (destination.ToString().Contains(command[1]))
                destination.Replace(command[1], command[2]);
        }
    }
}
