namespace Medicines.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Enums;
    using Common;
    public class Patient
    {
        public Patient()
        {
            this.PatientsMedicines = new HashSet<PatientMedicine>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.PatientFullNameMax)]
        public string FullName { get; set; } = null!;
        public AgeGroup AgeGroup { get; set; }
        public Gender Gender { get; set; }
        public virtual ICollection<PatientMedicine> PatientsMedicines { get; set; } = null!;
    }
}
