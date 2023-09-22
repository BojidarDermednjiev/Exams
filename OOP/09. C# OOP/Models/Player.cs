namespace Handball.Models
{
    using System;
    using Contracts;
    using Utilities.Messages;

    public abstract class Player : IPlayer
    {
        private string name;
        public Player(string name, double rating)
        {
            this.Name = name;
            this.Rating = rating;
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.PlayerNameNull));
                }
                name = value;
            }
        }
        public double Rating { get; protected set; }
        public string Team { get; protected set; }
        public void JoinTeam(string name)
        {
            Team = name;
        }
        public abstract void DecreaseRating();

        public abstract void IncreaseRating();
        public override string ToString()
         => $"{GetType().Name}: {Name}" + Environment.NewLine + $"--Rating: {Rating}";

    }
}
