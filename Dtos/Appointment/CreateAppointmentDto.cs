using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediConnectBackend.Dtos.Appointment
{
    public class CreateAppointmentDto
    {
        [Required]
        public string PatientId { get; set; } = string.Empty;

        [Required]
        public string DoctorId { get; set; } = string.Empty;

        [Required]
        public DateOnly AppointmentDate { get; set; }

        [Required]
        public TimeOnly AppointmentTime { get; set; }
        
        public string Notes { get; set; } = string.Empty;
    }
}