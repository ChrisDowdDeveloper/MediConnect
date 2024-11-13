using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MediConnectBackend.Models
{
    [Table("PastAppointments")]
    public class PastAppointment
    {
        public int Id { get; set; }

         public string PatientId { get; set; } = string.Empty;
        public Patient? Patient { get; set; }

        public string DoctorId { get; set; } = string.Empty;
        public Doctor? Doctor { get; set; }

        public DateTime AppointmentDateTime { get; set; }

        public string? Notes { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public DateTime CompletionDate { get; set; }
    }

}