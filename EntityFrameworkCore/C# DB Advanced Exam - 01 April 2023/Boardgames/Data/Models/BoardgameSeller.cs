namespace Boardgames.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    // ReSharper disable once IdentifierTypo
    public class BoardgameSeller
    {
        [ForeignKey(nameof(Boardgame))]

        // ReSharper disable once IdentifierTypo
        public int BoardgameId { get; set; }

        // ReSharper disable once IdentifierTypo
        public virtual Boardgame Boardgame  { get; set; } = null!;

        [ForeignKey(nameof(Seller))]
        public int SellerId  { get; set; }
        public virtual Seller Seller  { get; set; } = null!;

    }
}
