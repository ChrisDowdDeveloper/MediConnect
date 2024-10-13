using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Doctor;
using MediConnectBackend.Models;

namespace MediConnectBackend.Mappers
{
    public class DoctorMapper
    {
        public static Doctor ToModel(CreateDoctorRequestDto dto)
        {
            return new Doctor
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Specialty = dto.Specialty,
                Availability = dto.Availability,
                YearsOfExperience = dto.YearsOfExperience,
                OfficeAddress = dto.OfficeAddress
            };
        }

        public static DoctorResponseDto ToDto(Doctor doctor)
        {
            return new DoctorResponseDto
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialty = doctor.Specialty,
                Availability = doctor.Availability,
                YearsOfExperience = doctor.YearsOfExperience,
                OfficeAddress = doctor.OfficeAddress,
                Appointments = doctor.Appointments?.ToList() ?? [],
                PastAppointments = doctor.PastAppointments?.ToList() ?? []
            };
        }

        public static void UpdateModel(Doctor doctor, UpdateDoctorDto doctorDto)
        {
            doctor.FirstName = doctorDto.FirstName;
            doctor.LastName = doctorDto.LastName;
            doctor.Specialty = doctorDto.Specialty;
            doctor.Availability = doctorDto.Availability;
            doctor.YearsOfExperience = doctorDto.YearsOfExperience;
            doctor.OfficeAddress = doctorDto.OfficeAddress;
        }

    }
}