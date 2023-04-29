namespace _01._The_Imitation_Game
{
    using System;
    using System.Linq;
    using System.Text;
    public class StartUp
    {
        static void Main()
        {
            StringBuilder outputMessage = new StringBuilder(Console.ReadLine());
            Engine(Console.ReadLine(), outputMessage);
            IO(outputMessage);
        }
        private static StringBuilder Engine(string inputLineFromConsole, StringBuilder outputMessage)
        {
            if (inputLineFromConsole == "Decode")
                return outputMessage;
            var command = inputLineFromConsole.Split("|").First();
            switch (command)
            {
                case "ChangeAll":
                    ChangeAll(inputLineFromConsole, outputMessage);
                    break;
                case "Insert":
                    Insert(inputLineFromConsole, outputMessage);
                    break;
                case "Move":
                    Move(inputLineFromConsole, outputMessage);
                    break;
            }
            return Engine(Console.ReadLine(), outputMessage);
        }
        private static void ChangeAll(string inputLineFromConsole, StringBuilder outputMessage)
        {
            var substring = inputLineFromConsole.Split("|").Skip(1).First();
            var replacement = inputLineFromConsole.Split("|").Last();
            outputMessage.Replace(substring, replacement);
        }

        private static void Insert(string inputLineFromConsole, StringBuilder outputMessage)
        {
            var index = int.Parse(inputLineFromConsole.Split("|").Skip(1).First());
            var symbolToInsert = inputLineFromConsole.Split("|").Last();
            if (IsValidInex(index, outputMessage))
                outputMessage.Insert(index, symbolToInsert);
        }
        private static void Move(string inputLineFromConsole, StringBuilder outputMessage)
        {
            int indexToMove = int.Parse(inputLineFromConsole.Split("|").Last());
            if (IsValidInex(indexToMove, outputMessage))
            {
                var substring = String.Join(String.Empty, outputMessage.ToString().Take(indexToMove));
                outputMessage.Remove(0, indexToMove);
                outputMessage.Append(substring);
            }
        }
        private static bool IsValidInex(int index, StringBuilder outputMessage)
            => index < 0 && index > outputMessage.Length ? false : true;
        private static void IO(StringBuilder outputMessage)
        {
            Console.WriteLine($"The decrypted message is: {outputMessage}");
        }

    }
}
