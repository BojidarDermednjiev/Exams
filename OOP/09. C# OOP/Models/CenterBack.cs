namespace Handball.Models
{
    public class CenterBack : Player
    {
        public CenterBack(string name) : base(name, 4){}

        public override void DecreaseRating()
        {
            Rating -= 1;
            if (Rating < 1)
                Rating = 1;
        }

        public override void IncreaseRating()
        {
            Rating += 1;
            if (Rating > 10)
                Rating = 10;
        }
    }
}
