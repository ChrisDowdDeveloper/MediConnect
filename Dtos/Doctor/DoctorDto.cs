using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Appointment;
using MediConnectBackend.Dtos.Availability;
using MediConnectBackend.Dtos.PastAppointment;
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
        public int YearsOfExperience { get; set; }
        public string OfficeAddress { get; set; } = string.Empty;
        public List<AvailabilityDto> Availabilities { get; set; }
        public List<AppointmentDto>? Appointments { get; set; }
        public List<PastAppointmentDto>? PastAppointments { get; set; }
    }
}