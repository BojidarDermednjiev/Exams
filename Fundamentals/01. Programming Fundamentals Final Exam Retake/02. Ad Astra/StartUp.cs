namespace _02._Ad_Astra
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class StartUp
    {
        static void Main()
        {
            var pattern = @"([#\|]){1}(?<itemName>[A-z\s]+)\1(?<expirationDate>\d{2}\/\d{2}\/\d{2})\1(?<calories>\d+)\1";
            int totalCalories = default;
            var outputMessage = new List<string>();
            var inputLineFromConsole = Console.ReadLine();
            var matches = Regex.Matches(inputLineFromConsole, pattern);
            for (int currentLine = 0; currentLine < matches.Count; currentLine++)
            {
                var name = matches[currentLine].Groups["itemName"].Value;
                var date = matches[currentLine].Groups["expirationDate"].Value;
                var calories = int.Parse(matches[currentLine].Groups["calories"].Value);
                totalCalories += calories;
                outputMessage.Add($"Item: {name}, Best before: {date}, Nutrition: {calories}");
            }
            var days = totalCalories / 2000;
            Console.WriteLine($"You have food to last you for: {days} days!");
            outputMessage.ForEach(x => Console.WriteLine(x));
        }
    }
}
