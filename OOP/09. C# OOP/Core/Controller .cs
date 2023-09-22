namespace Handball.Core
{
    using System.Text;
    using System.Linq;
    using Contracts;
    using Repositories;
    using Handball.Models;
    using Models.Contracts;
    using Repositories.Contracts;
    using Handball.Utilities.Messages;

    public class Controller : IController
    {
        private IRepository<IPlayer> players;
        private IRepository<ITeam> teams;

        public Controller()
        {
            players = new PlayerRepository();
            teams = new TeamRepository();
        }
        public string NewTeam(string name)
        {
            ITeam team = new Team(name);
            if (teams.ExistsModel(team.Name))
                return string.Format(OutputMessages.TeamAlreadyExists, name, team.Name);
            teams.AddModel(team);
            return string.Format(OutputMessages.TeamSuccessfullyAdded, name, team.Name);
        }
        public string NewPlayer(string typeName, string name)
        {
            if (!new string[] { "Goalkeeper", "CenterBack", "ForwardWing" }.Contains(typeName))
                return string.Format(OutputMessages.InvalidTypeOfPosition, typeName);
            var type = players.GetModel(name);
            if (players.ExistsModel(name))
                return string.Format(OutputMessages.PlayerIsAlreadyAdded, name, typeName, type.GetType().Name);
            IPlayer player = typeName switch
            {
                nameof(Goalkeeper) => new Goalkeeper(name),
                nameof(CenterBack) => new CenterBack(name),
                nameof(ForwardWing) => new ForwardWing(name),
            };
            players.AddModel(player);
            return string.Format(OutputMessages.PlayerAddedSuccessfully, player.Name);
        }
        public string NewContract(string playerName, string teamName)
        {
            if (!players.ExistsModel(playerName))
                return string.Format(OutputMessages.PlayerNotExisting, playerName, nameof(PlayerRepository));
            if (!teams.ExistsModel(teamName))
                return string.Format(OutputMessages.TeamNotExisting, teamName, nameof(TeamRepository));
            var player = players.GetModel(playerName);
            var team = teams.GetModel(teamName);
            if (player.Team != null)
                return string.Format(OutputMessages.PlayerAlreadySignedContract, playerName, player.Team);
            player.JoinTeam(teamName);
            team.SignContract(player);
            return string.Format(OutputMessages.SignContract, playerName, teamName);

        }
        public string NewGame(string firstTeamName, string secondTeamName)
        {
            var firstTeam = teams.GetModel(firstTeamName);
            var secondTeam = teams.GetModel(secondTeamName);
            if (firstTeam.PointsEarned > secondTeam.PointsEarned)
            {
                firstTeam.Win();
                secondTeam.Lose();
                return string.Format(OutputMessages.GameHasWinner, firstTeam.Name, secondTeam.Name);
            }
            else if (firstTeam.PointsEarned < secondTeam.PointsEarned)
            {
                firstTeam.Lose();
                secondTeam.Win();
                return string.Format(OutputMessages.GameHasWinner, secondTeam.Name, firstTeam.Name);
            }
            else
            {
                firstTeam.Draw();
                secondTeam.Draw();
                return string.Format(OutputMessages.GameIsDraw, firstTeam.Name, secondTeam.Name);
            }
        }
        public string PlayerStatistics(string teamName)
        {
            var sb = new StringBuilder();
            var team = teams.GetModel(teamName);
            sb.AppendLine($"***{team.Name}***");
            var sortedPlayers = team.Players.OrderByDescending(x => x.Rating).ThenBy(x => x.Name).ToList();
            foreach (var player in sortedPlayers)
                sb.AppendLine(player.ToString());
            return sb.ToString().TrimEnd();
        }
        public string LeagueStandings()
        {
            var sb = new StringBuilder();
            var sortedTeams = teams.Models.OrderByDescending(x => x.PointsEarned).ThenByDescending(x => x.OverallRating).ThenBy(x => x.Name).ToList();
            sb.AppendLine($"***League Standings***");
            foreach (var team in sortedTeams)
                sb.AppendLine(team.ToString());
            return sb.ToString().TrimEnd();
        }
    }
}
