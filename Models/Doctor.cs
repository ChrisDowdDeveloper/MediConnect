using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MediConnectBackend.Models
{
    public class Doctor : User
    {
        public required string Specialty { get; set; }

        public required string Availability { get; set; }

        public required int YearsOfExperience { get; set; }

        public required string OfficeAddress { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }

        public ICollection<PastAppointment>? PastAppointments { get; set; }

    }
}