namespace _03_
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class Concert
    {
        private Dictionary<string, HashSet<string>> bands;
        private Dictionary<string, int> bandPlayTime;
        public Concert()
        {
            bands = new Dictionary<string, HashSet<string>>();
            bandPlayTime = new Dictionary<string, int>();
        }
        public Dictionary<string, HashSet<string>> Bands { get => bands; private set => bands = value; }
        public Dictionary<string, int> BandPlayTime { get => bandPlayTime; private set => bandPlayTime = value; }
        public void Add(List<string> songs, string[] inputLineFromConsole)
        {
            string nameOfBand = inputLineFromConsole.Skip(1).First();
            if (!Bands.Any(x => x.Key == nameOfBand))
                Bands.Add(nameOfBand, new HashSet<string>());
            foreach (var band in songs)
                Bands[nameOfBand].Add(band);
        }
        public void Play(string[] inputLineFromConsole)
        {
            var nameOfBand = inputLineFromConsole.Skip(1).First();
            var timePlayed = int.Parse(inputLineFromConsole.Last());
            if (!BandPlayTime.ContainsKey(nameOfBand))
                bandPlayTime.Add(nameOfBand, default);
            bandPlayTime[nameOfBand] += timePlayed;
        }
        public override string ToString()
        {
            int totalTime = default;
            foreach (var currentTime in BandPlayTime.Values)
                totalTime += currentTime;
            var sb = new StringBuilder();
            sb.AppendLine($"Total time: {totalTime}");
            foreach (var time in BandPlayTime.OrderBy(x => x.Value))
                sb.AppendLine($"{time.Key} -> {time.Value}");
            foreach (var band in Bands.Take(1))
            {
                sb.AppendLine($"{band.Key}");
                foreach (var song in band.Value)
                    sb.AppendLine($"=> {song}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
