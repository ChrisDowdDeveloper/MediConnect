using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Models;

namespace MediConnectBackend.Dtos.Doctor
{
    public class DoctorResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string Availability { get; set; } = string.Empty;
        public int YearsOfExperience { get; set; }
        public string OfficeAddress { get; set; } = string.Empty;
        public ICollection<MediConnectBackend.Models.Appointment>? Appointments { get; set; } = [];
        public ICollection<MediConnectBackend.Models.PastAppointment>? PastAppointments { get; set; } = [];
    }
}