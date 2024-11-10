using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediConnectBackend.Dtos.TimeSlot
{
    public class UpdateTimeSlotDto
    {
        [Required]
        public int Id { get; set; }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool IsBooked { get; set; }
        public int AppointmentId { get; set; }
    }
}