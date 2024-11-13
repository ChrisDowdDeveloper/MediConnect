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
        public required int YearsOfExperience { get; set; }
        public required string OfficeAddress { get; set; }
        
        public List<Availability> Availabilities { get; set; } = new List<Availability>();
        public List<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public List<PastAppointment> PastAppointments { get; set; } = new List<PastAppointment>();

    }
}