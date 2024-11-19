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
        public static PastAppointmentDto ToDto(PastAppointment pastAppointment)
        {
            return new PastAppointmentDto
            {
                Id = pastAppointment.Id,
                PatientId = pastAppointment.PatientId,
                DoctorId = pastAppointment.DoctorId,
                AppointmentDateTime = pastAppointment.AppointmentDateTime,
                Notes = pastAppointment.Notes,
                CompletionDate = pastAppointment.CompletionDate
            };
        }
    }
}