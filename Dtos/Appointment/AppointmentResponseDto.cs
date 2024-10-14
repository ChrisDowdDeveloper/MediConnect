using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Models;

namespace MediConnectBackend.Dtos.Appointment
{
    public class AppointmentResponseDto
    {
        public int Id { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string DoctorId { get; set; } = string.Empty;
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public string? Notes { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}