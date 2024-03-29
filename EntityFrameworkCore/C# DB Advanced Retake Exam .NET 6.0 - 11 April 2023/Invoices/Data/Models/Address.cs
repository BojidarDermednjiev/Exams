﻿namespace Invoices.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common;

    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.AddressStreetNameMaxLength)]
        public string StreetName { get; set; } = null!;
        
        [Required]
        public int StreetNumber { get; set; }

        [Required] 
        public string PostCode { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.AddressCityNameMaxLength)]
        public string City { get; set; } = null!;

        [Required]
        [MaxLength(ValidationConstants.AddressCountryMaxName)]
        public string Country { get; set; } = null!;

        [ForeignKey(nameof(Client))]
        public int ClientId { get; set; }
        public virtual Client Client { get; set; } = null!;
    }
}
