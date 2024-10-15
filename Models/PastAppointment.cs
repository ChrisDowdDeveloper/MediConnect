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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PastAppointmentId { get; set; }

        public string? PatientId { get; set; }
        public Patient? Patient { get; set; }

        public string? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public DateTime AppointmentDateTime { get; set; }

        public string? Notes { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public DateTime CompletionDate { get; set; }
    }

}