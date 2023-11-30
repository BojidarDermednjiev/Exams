﻿namespace Artillery.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class Country
    {
        public Country()
        {
            this.CountriesGuns = new HashSet<CountryGun>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CountryNameMaxLength)]
        public string CountryName { get; set; } = null!;
        public int ArmySize  { get; set; }
        public virtual ICollection<CountryGun> CountriesGuns { get; set; } = null!;
    }
}
