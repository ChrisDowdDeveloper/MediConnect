using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Appointment;
using MediConnectBackend.Dtos.Availability;
using MediConnectBackend.Dtos.Doctor;
using MediConnectBackend.Dtos.PastAppointment;
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
                Availabilities = dto.Availabilities?.Select(AvailabilityMapper.ToModel).ToList(),
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
                UserName = doctor.UserName,
                PhoneNumber = doctor.PhoneNumber,
                Specialty = doctor.Specialty,
                YearsOfExperience = doctor.YearsOfExperience,
                OfficeAddress = doctor.OfficeAddress,
                
                Availabilities = doctor.Availabilities != null 
                ? doctor.Availabilities.Select(AvailabilityMapper.ToDto).ToList() 
                : new List<AvailabilityResponseDto>(),

            Appointments = doctor.Appointments != null 
                ? doctor.Appointments.Select(AppointmentMapper.ToDto).ToList() 
                : new List<AppointmentResponseDto>(),

                PastAppointments = doctor.PastAppointments?.Select(pastAppointment => new PastAppointmentResponseDto
                {
                    Id = pastAppointment.PastAppointmentId,
                    PatientId = pastAppointment.PatientId,
                    DoctorId = pastAppointment.DoctorId,
                    AppointmentDateTime = pastAppointment.AppointmentDateTime,
                    Notes = pastAppointment.Notes,
                    CompletionDate = pastAppointment.CompletionDate
                }).ToList() ?? []
            };
        }

        public static void UpdateModel(Doctor doctor, UpdateDoctorDto doctorDto)
        {
            if (!string.IsNullOrEmpty(doctorDto.FirstName))
                doctor.FirstName = doctorDto.FirstName;

            if (!string.IsNullOrEmpty(doctorDto.LastName))
                doctor.LastName = doctorDto.LastName;

            if (!string.IsNullOrEmpty(doctorDto.Specialty))
                doctor.Specialty = doctorDto.Specialty;

            if (doctorDto.YearsOfExperience.HasValue)
                doctor.YearsOfExperience = doctorDto.YearsOfExperience.Value;

            if (!string.IsNullOrEmpty(doctorDto.OfficeAddress))
                doctor.OfficeAddress = doctorDto.OfficeAddress;

            if (doctorDto.Availabilities != null && doctor.Availabilities != null)
            {
                foreach (var dto in doctorDto.Availabilities)
                {
                    var existingAvailability = doctor.Availabilities.FirstOrDefault(a => a.Id == dto.Id);
                    if (existingAvailability != null)
                    {
                        AvailabilityMapper.UpdateModel(existingAvailability, dto);
                    }
                    else
                    {
                        doctor.Availabilities.Add(AvailabilityMapper.ToModel(dto));
                    }
                }
            }
        }
    }
}