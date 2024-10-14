using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediConnectBackend.Helpers
{
    public class AppointmentQueryObject
    {
        public string? DoctorId {get; set;}
        public string? PatientId {get; set;}
        public bool IsDescending { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}