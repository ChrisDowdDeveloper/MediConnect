using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Models;

namespace MediConnectBackend.Helpers
{
    public class DoctorQueryObject
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Specialty { get; set; }
        public ICollection<Availability>? Availabilities { get; set; }
        public int YearsOfExperience { get; set; }
        public string? OfficeAddress { get; set; }
        public bool IsDescending { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}