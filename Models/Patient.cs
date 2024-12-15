using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MediConnectBackend.Models
{
    public class Patient : User
    {
        public required DateTime DateOfBirth { get; set; }

        public required string Gender { get; set; }

        public required string Address { get; set; }

        public required string EmergencyContactFirstName { get; set; }
        public required string EmergencyContactLastName { get; set; }
        public required string EmergencyContactPhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public List<PastAppointment> PastAppointments { get; set; } = new List<PastAppointment>();

    }
}