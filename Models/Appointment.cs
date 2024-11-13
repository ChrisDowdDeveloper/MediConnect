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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? PatientId { get; set; }
        public Patient? Patient { get; set; }

        public string? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public DateTime AppointmentDateTime { get; set; }

        public AppointmentStatus AppointmentStatus { get; set; } = AppointmentStatus.BOOKED;

        public string? Notes { get; set; }

        public int? TimeSlotId { get; set; }
        public TimeSlot? TimeSlot { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedDate { get; set; } = DateTime.UtcNow;

    }

    public enum AppointmentStatus
    {
        BOOKED,
        CANCELED,
        RESCHEDULED,
        FINISHED
    }
}