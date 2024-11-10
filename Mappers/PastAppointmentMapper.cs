using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.PastAppointment;
using MediConnectBackend.Models;

namespace MediConnectBackend.Mappers
{
    public class PastAppointmentMapper
    {
        public static PastAppointmentResponseDto ToDto(PastAppointment pastAppointment)
        {
            return new PastAppointmentResponseDto
            {
                Id = pastAppointment.PastAppointmentId,
                PatientId = pastAppointment.PatientId,
                DoctorId = pastAppointment.DoctorId,
                AppointmentDateTime = pastAppointment.AppointmentDateTime,
                Notes = pastAppointment.Notes,
                CompletionDate = pastAppointment.CompletionDate
            };
        }
    }
}