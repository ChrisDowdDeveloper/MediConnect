using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediConnectBackend.Dtos.TimeSlot
{
    public class TimeSlotDto
    {
        public int Id { get; set; }
        public string DoctorId { get; set; } = string.Empty;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public bool IsBooked { get; set; }
        public int? AppointmentId { get; set; }
    }
}