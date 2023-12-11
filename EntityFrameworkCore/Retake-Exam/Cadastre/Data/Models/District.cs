namespace Cadastre.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    
    using Common;
    using Enumerations;
    public class District
    {
        public District()
        {
            this.Properties = new HashSet<Property>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DistrictNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public string PostalCode { get; set; } = null!;

        [Required]
        public Region Region { get; set; }
        public virtual ICollection<Property> Properties { get; set; } = null!;
    }
}
