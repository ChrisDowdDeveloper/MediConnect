using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Patient;
using MediConnectBackend.Models;

namespace MediConnectBackend.Mappers
{
    public class PatientMapper
    {
        public static Patient ToModel(CreatePatientRequestDto dto)
        {
            return new Patient
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Address = dto.Address,
                EmergencyContactFirstName = dto.EmergencyContactFirstName,
                EmergencyContactLastName = dto.EmergencyContactLastName,
                EmergencyContactPhoneNumber = dto.EmergencyContactPhoneNumber,
                RegistrationDate = DateTime.Now,
                Email = dto.Email
            };
        }

        public static PatientResponseDto ToDto(Patient patient)
        {
            return new PatientResponseDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                Address = patient.Address,
                EmergencyContactFirstName = patient.EmergencyContactFirstName,
                EmergencyContactLastName = patient.EmergencyContactLastName,
                EmergencyContactPhoneNumber = patient.EmergencyContactPhoneNumber,
                Appointments = patient.Appointments?.ToList() ?? [],
                PastAppointments = patient.PastAppointments?.ToList() ?? []
            };
        }
    }
}