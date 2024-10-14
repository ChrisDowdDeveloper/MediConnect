using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Appointment;
using MediConnectBackend.Models;

namespace MediConnectBackend.Mappers
{
    public class AppointmentMapper
    {
        public static Appointment ToModel(CreateAppointmentDto dto)
        {
            return new Appointment
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                AppointmentDate = dto.AppointmentDate,
                AppointmentTime = dto.AppointmentTime,
                Notes = dto.Notes
            };
        }

        public static AppointmentResponseDto ToDto(Appointment appointment)
        {
            if(string.IsNullOrEmpty(appointment.DoctorId))
            {
                throw new InvalidOperationException("DoctorId cannot be null");
            }

            if(string.IsNullOrEmpty(appointment.PatientId))
            {
                throw new InvalidOperationException("PatientId cannot be null");
            }

            return new AppointmentResponseDto
            {
                Id = appointment.AppointmentId,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentStatus = appointment.AppointmentStatus,
                Notes = appointment.Notes,
                CreationDate = appointment.CreationDate,
                LastUpdatedDate = appointment.LastUpdatedDate
            };
        }
        
        public static void UpdateModel(Appointment appointment, UpdateAppointmentDto dto)
        {
            appointment.PatientId = dto.PatientId;
            appointment.DoctorId = dto.DoctorId;
            appointment.AppointmentDate = dto.AppointmentDate;
            appointment.AppointmentTime = dto.AppointmentTime;
            appointment.AppointmentStatus = dto.AppointmentStatus;
            appointment.Notes = dto.Notes;
            appointment.LastUpdatedDate = DateTime.UtcNow;
        }
    }
}