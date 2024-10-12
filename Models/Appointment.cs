using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MediConnectBackend.Models
{
    [Table("Appointments")]
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public string? PatientId { get; set; }
        public Patient? Patient { get; set; }

        public string? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }

        public AppointmentStatus AppointmentStatus { get; set; } = AppointmentStatus.BOOKED;

        public string? Notes { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedDate { get; set; } = DateTime.UtcNow;

    }

    public enum AppointmentStatus
    {
        BOOKED,
        CANCELED,
        RESCHEDULED
    }
}