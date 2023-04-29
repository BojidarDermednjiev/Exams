namespace _03._The_Pianist
{
    using System;
    using System.Collections.Generic;
    public class StartUp
    {
        static void Main()
        {
            string a = Console.ReadLine();
            List<Piece> pieces;
            Piece piece;
            GetInfo(out pieces, out piece);
            piece = PieceOperation(Console.ReadLine(), piece, pieces);
            IO(pieces, piece);
        }
        private static void GetInfo(out List<Piece> pieces, out Piece piece)
        {
            pieces = new List<Piece>();
            int numberOfPieces = int.Parse(Console.ReadLine());
            piece = new Piece();
            for (int currentPiece = 0; currentPiece < numberOfPieces; currentPiece++)
            {
                string[] piecesInfo = Console.ReadLine().Split('|', StringSplitOptions.RemoveEmptyEntries);
                piece = new Piece(piecesInfo[0], piecesInfo[1], piecesInfo[2]);
                pieces.Add(piece);
            }
        }
        private static Piece PieceOperation(string inputLineFromConsole, Piece piece, List<Piece> pieces)
        {
            if(inputLineFromConsole == "Stop")
                return piece;
            string[] tokens = inputLineFromConsole.Split('|', StringSplitOptions.RemoveEmptyEntries);
            switch (tokens[0])
            {
                case "Add":
                    piece.Add(pieces, tokens);
                    break;
                case "Remove":
                    piece.Remove(pieces, tokens);
                    break;
                case "ChangeKey":
                    piece.ChangeKey(pieces, tokens);
                    break;
            }
            return PieceOperation(Console.ReadLine(), piece, pieces);
        }
        private static void IO(List<Piece> pieces, Piece piece)
        {
            Console.WriteLine(piece.Report(pieces));
        }
    }
}
