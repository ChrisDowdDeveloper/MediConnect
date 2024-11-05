using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediConnectBackend.Dtos.Availability
{
    public class CreateAvailabilityRequestDto
    {
        public string? DoctorId { get; set; }

        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }
        
        [Required]
        public TimeSpan EndTime { get; set; }

        public bool IsRecurring { get; set; } = false;
    }
}