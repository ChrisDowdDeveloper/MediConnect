using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediConnectBackend.Models
{
    public class Availability
    {
        public int Id { get; set; }
        public string? DoctorId {get; set;}
        public Doctor? Doctor {get; set;}

        public DayOfWeek DayOfWeek {get; set;}
        public TimeSpan StartTime {get; set;}
        public TimeSpan EndTime {get; set;}
        public bool IsRecurring {get; set;} = false;
    }
}