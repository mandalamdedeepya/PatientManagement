using System.ComponentModel.DataAnnotations;

namespace PatientManagement.Model
{
    public class Patient
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required, Phone]
        public string ContactNumber { get; set; }

        public float? Weight { get; set; }
        public float? Height { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }
        public string MedicalComments { get; set; }

        [Required]
        public bool AnyMedicationsTaking { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
