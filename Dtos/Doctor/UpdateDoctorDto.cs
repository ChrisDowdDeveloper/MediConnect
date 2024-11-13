using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Appointment;
using MediConnectBackend.Dtos.Availability;

namespace MediConnectBackend.Dtos.Doctor
{
    public class UpdateDoctorDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public int? YearsOfExperience { get; set; }
        public string OfficeAddress { get; set; } = string.Empty;

        public List<UpdateAvailabilityDto> Availabilities { get; set; }
    }
}