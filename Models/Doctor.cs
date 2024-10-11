using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MediConnectBackend.Models
{
    public class Doctor : IdentityUser
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Specialty { get; set; }

        public required string Availability { get; set; }

        public required int YearsOfExperience { get; set; }

        public required string OfficeAddress { get; set; }

        public required string Username { get; set; }

    }
}