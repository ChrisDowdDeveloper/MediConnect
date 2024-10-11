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
        
        public int PastAppointmentId { get; set; }

        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }

        public PastAppointmentStatus PastAppointmentStatus { get; set; } = PastAppointmentStatus.COMPLETED;

        public string? DoctorNotes { get; set; }

        public int? AppointmentDuration { get; set; }

    }

    public enum PastAppointmentStatus
    {
        COMPLETED,
        NO_SHOW
    }
}