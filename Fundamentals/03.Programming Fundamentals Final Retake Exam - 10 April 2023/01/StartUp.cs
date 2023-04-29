namespace _01_
{
    using System;
    using System.Text;

    public class StartUp
    {
        static void Main()
        {
            string inputLine = Console.ReadLine();
            var skill = new StringBuilder(inputLine);
            string inputLineFromConsole;
            ;
            while ((inputLineFromConsole = Console.ReadLine()) != "For Azeroth")
            {
                var command = inputLineFromConsole.Split();
                switch (command[0])
                {
                    case "GladiatorStance":
                        {
                            skill.Clear();
                            skill.Append(inputLine.ToUpper());
                            Console.WriteLine(skill);
                        }
                        break;
                    case "DefensiveStance":
                        {
                            skill.Clear();
                            skill.Append(inputLine.ToLower());
                            Console.WriteLine(skill);
                        }
                        break;
                    case "Dispel":
                        Dispel(command, skill);
                        break;
                    case "Target":
                        if (command[1] == "Change")
                            Change(command, skill);
                        else
                            Remove(command, skill);
                        break;
                    default:
                        Console.WriteLine("Command doesn't exist!");
                        break;
                }
                inputLine = skill.ToString();
            }

        }
        private static void Dispel(string[] command, StringBuilder skill)
        {
            var index = int.Parse(command[1]);
            if (index < 0 || index > skill.Length)
            {
                Console.WriteLine("Dispel too weak.");
                return;
            }
            skill.Remove(index, 1);
            skill.Insert(index, command[2]);
            Console.WriteLine("Success!");
        }

        private static void Change(string[] command, StringBuilder skill)
        {
            var firstSpell = command[2];
            var secondSpell = command[3];
            if (skill.ToString().Contains(firstSpell))
                skill.Replace(firstSpell, secondSpell);
            Console.WriteLine(skill);
        }

        private static void Remove(string[] command, StringBuilder skill)
        {
            var removeItem = command[2];
            var indexFromItem = skill.ToString().IndexOf(removeItem);
            if (skill.ToString().Contains(removeItem))
            {
                skill.Remove(indexFromItem, removeItem.Length);
                Console.WriteLine(skill);
            }
        }
    }
}
