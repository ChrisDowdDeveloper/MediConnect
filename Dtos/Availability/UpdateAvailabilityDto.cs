using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MediConnectBackend.Dtos.Availability
{
    public class UpdateAvailabilityDto
    {
        [Required]
        public int Id { get; set; }

        public DayOfWeek? DayOfWeek { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool? IsRecurring { get; set; }
    }
}