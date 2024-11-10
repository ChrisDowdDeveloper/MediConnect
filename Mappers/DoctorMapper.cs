using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Data;
using MediConnectBackend.Dtos.Appointment;
using MediConnectBackend.Dtos.Availability;
using MediConnectBackend.Dtos.Doctor;
using MediConnectBackend.Dtos.PastAppointment;
using MediConnectBackend.Models;

namespace MediConnectBackend.Mappers
{
    public class DoctorMapper
    {
        public static DoctorResponseDto ToDoctorDto(Doctor doctorModel)
        {
            return new DoctorResponseDto
            {
                Id = doctorModel.Id,
                UserName = doctorModel.UserName ?? "",
                FirstName = doctorModel.FirstName,
                LastName = doctorModel.LastName,
                PhoneNumber = doctorModel.PhoneNumber ?? "",
                Specialty = doctorModel.Specialty,
                YearsOfExperience = doctorModel.YearsOfExperience,
                OfficeAddress = doctorModel.OfficeAddress,
                Availabilities = doctorModel.Availabilities.Select(a => AvailabilityMapper.ToDto(a)).ToList(),
                Appointments = doctorModel.Appointments.Select(ap => AppointmentMapper.ToDto(ap)).ToList(),
                PastAppointments = doctorModel.PastAppointments.Select(pa => PastAppointmentMapper.ToDto(pa)).ToList(),
            };
        }

        public static Doctor ToDoctorFromCreateDto(CreateDoctorRequestDto doctorDto)
        {
            return new Doctor
            {
                FirstName = doctorDto.FirstName,
                LastName = doctorDto.LastName,
                Specialty = doctorDto.Specialty,
                YearsOfExperience = doctorDto.YearsOfExperience,
                OfficeAddress = doctorDto.OfficeAddress,
            };
        }

    }
}