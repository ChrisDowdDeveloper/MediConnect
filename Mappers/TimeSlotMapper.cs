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
        public static TimeSlot ToModel(CreateTimeSlotRequestDto dto)
        {
            return new TimeSlot
            {
                DoctorId = dto.DoctorId,
                StartDateTime = dto.StartDateTime,
                EndDateTime = dto.EndDateTime,
                IsBooked = dto.IsBooked,
                AppointmentId = dto.AppointmentId
            };
        }

        public static TimeSlotResponseDto ToDto(TimeSlot timeSlot)
        {
            if(string.IsNullOrEmpty(timeSlot.DoctorId))
            {
                throw new InvalidOperationException("DoctorId cannot be null");
            }
            return new TimeSlotResponseDto
            {
                Id = timeSlot.Id,
                DoctorId = timeSlot.DoctorId,
                StartDateTime = timeSlot.StartDateTime,
                EndDateTime = timeSlot.EndDateTime,
                IsBooked = timeSlot.IsBooked
            };
        }

        public static void UpdateModel(TimeSlot timeSlot, UpdateTimeSlotDto dto)
        {
            timeSlot.StartDateTime = dto.StartDateTime;
            timeSlot.EndDateTime = dto.EndDateTime;
            timeSlot.IsBooked = dto.IsBooked ?? timeSlot.IsBooked;
        }
    }

}