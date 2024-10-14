using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
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
        public DateTime AppointmentDateTime { get; set; }
        
        public string Notes { get; set; } = string.Empty;
    }
}