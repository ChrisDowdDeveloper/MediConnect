using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediConnectBackend.Dtos.Patient
{
    public class CreatePatientRequestDto
    {
        [Required, MinLength(2)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MinLength(2)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public required string Gender { get; set; } = string.Empty;

        [Required]
        public required string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        [Required, MinLength(2)]
        public required string EmergencyContactFirstName { get; set; } = string.Empty;

        [Required, MinLength(2)]
        public required string EmergencyContactLastName { get; set; } = string.Empty;

        [Required]
        public required string EmergencyContactPhoneNumber { get; set; } = string.Empty;

        [Required]
        public DateTime RegistrationDate { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}