using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediConnectBackend.Dtos.Patient
{
    public class UpdatePatientDto
    {
        public string Id { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string EmergencyContactFirstName { get; set; } = string.Empty;
        public string EmergencyContactLastName { get; set; } = string.Empty;
        public string EmergencyContactPhoneNumber { get; set; } = string.Empty;
    }
}