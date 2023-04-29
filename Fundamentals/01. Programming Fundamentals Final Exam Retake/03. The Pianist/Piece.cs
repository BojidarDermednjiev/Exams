namespace _03._The_Pianist
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class Piece
    {
        private string name;
        private string composer;
        private string key;
        private List<Piece> pieces;
        public Piece() { }
        public Piece(string name, string composer, string key) : this()
        {
            Name = name;
            Composer = composer;
            Key = key;
            pieces = new List<Piece>();
        }
        public string Name { get => name; private set => name = value; }
        public string Composer { get => composer; private set => composer = value; }
        public string Key { get => key; private set => key = value; }
        public List<Piece> Pieces { get => pieces; private set => pieces = value; }
        public void Add(List<Piece> pieces, string[] tokens)
        {
            var name = tokens[1];
            var composer = tokens[2];
            var key = tokens[3];
            if (pieces.Any(x => x.Name == name))
                Console.WriteLine($"{name} is already in the collection!");
            else
            {
                pieces.Add(new Piece(name, composer, key));
                Console.WriteLine($"{name} by {composer} in {key} added to the collection!");
            }
        }
        public void Remove(List<Piece> pieces, string[] tokens)
        {
            var piece = tokens[1];
            if (pieces.Any(x => x.Name == piece))
            {
                Piece pieceToRemove = pieces.Find(x => x.Name == piece);
                Console.WriteLine($"Successfully removed {pieceToRemove.Name}!");
                pieces.Remove(pieceToRemove);
            }
            else
                Console.WriteLine($"Invalid operation! {piece} does not exist in the collection.");
        }
        public void ChangeKey(List<Piece> pieces, string[] tokens)
        {
            var name = tokens[1];
            var newKey = tokens[2];
            if (pieces.Any(x => x.Name == name))
            {
                pieces.First(x => x.Name == name).Key = newKey;
                Console.WriteLine($"Changed the key of {name} to {newKey}!");
            }
            else
                Console.WriteLine($"Invalid operation! {name} does not exist in the collection.");
        }
        public string Report(List<Piece> pieces)
            => String.Join(Environment.NewLine, pieces.Select(x => $"{x.Name} -> Composer: {x.Composer}, Key: {x.Key}"));
    }
}