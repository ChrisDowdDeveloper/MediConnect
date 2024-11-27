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
        public static TimeSlotDto ToDto(TimeSlot timeSlot)
        {
            return new TimeSlotDto
            {
                Id = timeSlot.Id,
                DoctorId = timeSlot.DoctorId,
                StartTime = timeSlot.StartTime,
                EndTime = timeSlot.EndTime,
                IsBooked = timeSlot.IsBooked
            };
        }

        public static TimeSlot ToTimeSlotFromCreateDTO(string doctorId, CreateTimeSlotRequestDto dto)
        {
            return new TimeSlot
            {
                DoctorId = doctorId,
                AvailabilityId = dto.AvailabilityId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsBooked = dto.IsBooked,
                AppointmentId = dto.AppointmentId
            };
        }
    }

}