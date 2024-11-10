using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Availability;

namespace MediConnectBackend.Dtos.Doctor
{
    public class CreateDoctorRequestDto
    {
        [Required, MinLength(2)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MinLength(2)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Specialty { get; set; } = string.Empty;

         public List<CreateAvailabilityRequestDto> Availabilities { get; set; } = [];

        [Required]
        public int YearsOfExperience { get; set; }

        [Required]
        public string OfficeAddress { get; set; } = string.Empty;

        [Required, MinLength(5)]
        public string UserName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;

    }
}