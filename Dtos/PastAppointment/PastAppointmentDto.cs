using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Doctor;

namespace MediConnectBackend.Dtos.PastAppointment
{
    public class PastAppointmentDto
    {
        public int Id { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string DoctorId { get; set; } = string.Empty;
        public DoctorAppointmentResponseDto? Doctor { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string? Notes { get; set; }
        public DateTime CompletionDate { get; set; }
    }

}