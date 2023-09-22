namespace Handball.Models
{
    public class Goalkeeper : Player
    {
        public Goalkeeper(string name) : base(name, 2.5) { }

        public override void DecreaseRating()
        {
            Rating -= 1.25;
            if (Rating < 1)
            {
                Rating = 1;
            }
        }
        public override void IncreaseRating()
        {
            Rating += 0.75;
            if (Rating > 10)
                Rating = 10;
        }
    }
}
