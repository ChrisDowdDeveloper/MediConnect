using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MediConnectBackend.Models
{
    public class Patient : IdentityUser
    {
        
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required DateOnly DateOfBirth { get; set; }

        public required string Gender { get; set; }

        public required string Address { get; set; }

        public required string EmergencyContactFirstName { get; set; }
        public required string EmergencyContactLastName { get; set; }
        public required string EmergencyContactPhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public required string Username { get; set; }

    }
}