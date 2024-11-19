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
                AppointmentDateTime = dto.AppointmentDateTime,
                Notes = dto.Notes
            };
        }

        public static AppointmentDto ToDto(Appointment appointment)
        {
            if(string.IsNullOrEmpty(appointment.DoctorId))
            {
                throw new InvalidOperationException("DoctorId cannot be null");
            }

            if(string.IsNullOrEmpty(appointment.PatientId))
            {
                throw new InvalidOperationException("PatientId cannot be null");
            }

            return new AppointmentDto
            {
                Id = appointment.Id,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                AppointmentDateTime = appointment.AppointmentDateTime,
                AppointmentStatus = appointment.AppointmentStatus.ToString(),
                Notes = appointment.Notes,
                CreationDate = appointment.CreationDate,
                LastUpdatedDate = appointment.LastUpdatedDate
            };
        }
    }
}