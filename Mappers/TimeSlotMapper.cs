using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.TimeSlot;
using MediConnectBackend.Models;

namespace MediConnectBackend.Mappers
{
    public static class TimeSlotMapper
    {
        public static TimeSlotResponseDto ToDto(TimeSlot timeSlot)
        {
            return new TimeSlotResponseDto
            {
                Id = timeSlot.Id,
                DoctorId = timeSlot.DoctorId,
                StartDateTime = timeSlot.StartDateTime,
                EndDateTime = timeSlot.EndDateTime,
                IsBooked = timeSlot.IsBooked
            };
        }

        public static TimeSlot ToTimeSlotFromCreateDTO(CreateTimeSlotRequestDto dto)
        {
            return new TimeSlot
            {
                DoctorId = dto.DoctorId,
                AvailabilityId = dto.AvailabilityId,
                StartDateTime = dto.StartDateTime,
                EndDateTime = dto.EndDateTime,
                IsBooked = dto.IsBooked,
                AppointmentId = dto.AppointmentId
            };
        }
    }

}