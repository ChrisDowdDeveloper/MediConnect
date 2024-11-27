using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediConnectBackend.Models
{
    public class TimeSlot
    {
        public int Id { get; set; }
        
        public string DoctorId { get; set; } = string.Empty;
        public Doctor? Doctor { get; set; }

        public int AvailabilityId { get; set; } 
        public Availability? Availability { get; set; }

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsBooked { get; set; } = false;

        public int? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
    }

}