using System;
using System.Collections.Generic;
using MediConnectBackend.Dtos.TimeSlot;

namespace MediConnectBackend.Dtos.Availability
{
    public class AvailabilityDto
    {
        public int Id { get; set; }
        public string DoctorId { get; set; } = string.Empty;
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsRecurring { get; set; }
        public List<TimeSlotDto>? TimeSlots { get; set; }
    }
}
