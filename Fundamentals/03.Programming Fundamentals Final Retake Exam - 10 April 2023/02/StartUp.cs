namespace _02_
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class StartUp
    {
        static void Main()
        {
            var numberOfEmployee = int.Parse(Console.ReadLine());
            var pattern = @"^(?<employee>[A-Z][a-z]+ [A-Z][a-z]+)[#]+(?<job>(([A-Z][a-z]+){1})|([A-Z][a-z]+(&[A-Z][a-z]+){1,2}))[0-9]{2}(?<company>[A-Z][a-z]*[A-z]*[0-9]* (JSC|Ltd.))$";
            List<string> outputMesage = new List<string>();
            for (int currentEmployee = 0; currentEmployee < numberOfEmployee; currentEmployee++)
            {
                var inputLineFromConsole = Console.ReadLine();
                if (Regex.IsMatch(inputLineFromConsole, pattern))
                {
                    var mathces = Regex.Matches(inputLineFromConsole, pattern);
                    var employee = mathces[0].Groups["employee"].Value;
                    var job = mathces[0].Groups["job"].Value;
                    var company = mathces[0].Groups["company"].Value;
                    outputMesage.Add($"{employee} is {string.Join(" ", job.Split("&"))} at {company}");
                }
            }
            outputMesage.ForEach(x => Console.WriteLine(x));
        }
    }
}
