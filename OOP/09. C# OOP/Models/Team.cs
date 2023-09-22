namespace Handball.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Utilities.Messages;

    public class Team : ITeam
    {
        private string name;
        private int pointsEarned;
        private List<IPlayer> players;

        public Team(string name)
        {
            Name = name;
            pointsEarned = 0;
            players = new List<IPlayer>();
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(string.Format(ExceptionMessages.TeamNameNull));
                name = value;
            }
        }

        public int PointsEarned
        {
            get => pointsEarned;
            private set => pointsEarned = value;
        }

        public double OverallRating
        {
            get
            {
                if (players.Count == 0)
                    return 0;
                double totalRating = 0;
                foreach (var player in players)
                    totalRating += player.Rating;
                return Math.Round(totalRating / players.Count, 2);
            }
        }

        public IReadOnlyCollection<IPlayer> Players => players.AsReadOnly();
        public void SignContract(IPlayer player)
            => players.Add(player);
        public void Win()
        {
            pointsEarned += 3;
            players.ForEach(x => x.IncreaseRating());
        }
        public void Lose()
        {
            players.ForEach(x => x.DecreaseRating());
        }
        public void Draw()
        {
            pointsEarned++;
            foreach (var player in players)
            {
                if (player.GetType().Name == nameof(Goalkeeper))
                    player.IncreaseRating();
            }
        }
        public override string ToString()
        {
            var names = players.Select(x => x.Name).ToList();
            string playerNames = players.Count == 0 ? "none" : string.Join(", ", names);
            return $"Team: {Name} Points: {PointsEarned}"
                    + Environment.NewLine
                    + $"--Overall rating: {OverallRating}"
                    + Environment.NewLine
                    + $"--Players: {playerNames}";
        }
    }
}
