using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediConnectBackend.Models
{
    public class TimeSlot
    {
        public int Id { get; set; }
        public string? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public int? AvailabilityId { get; set; }
        public Availability? Availability { get; set; }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool IsBooked { get; set; } = false;

        public int? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
    }

}