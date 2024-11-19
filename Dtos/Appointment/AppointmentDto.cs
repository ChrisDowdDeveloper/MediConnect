using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Models;

namespace MediConnectBackend.Dtos.Appointment
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string DoctorId { get; set; } = string.Empty;
        public DateTime AppointmentDateTime { get; set; }
        public string AppointmentStatus { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}