namespace Cadastre.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class PropertyCitizen
    {
        [ForeignKey(nameof(Property))]
        public int  PropertyId  { get; set; }
        public virtual Property Property { get; set; } = null!;

        [ForeignKey(nameof(Citizen))]
        public int  CitizenId   { get; set; }
        public virtual Citizen Citizen { get; set; } = null!;
    }
}
