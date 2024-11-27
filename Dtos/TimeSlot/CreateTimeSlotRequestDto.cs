using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediConnectBackend.Dtos.TimeSlot
{
    public class CreateTimeSlotRequestDto
    {
        [Required]
        public string DoctorId { get; set; } = string.Empty;

        public int AvailabilityId { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }
        
        [Required]
        public TimeOnly EndTime { get; set; }

        public bool IsBooked { get; set; }
        public int? AppointmentId { get; set; }
    }
}