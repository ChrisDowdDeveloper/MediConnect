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
        public DateTime StartDateTime { get; set; }
        
        [Required]
        public DateTime EndDateTime { get; set; }

        public bool IsBooked { get; set; }
        
        [Required]
        public int AppointmentId { get; set; }
    }
}