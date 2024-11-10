using System;
using System.Collections.Generic;
using System.Linq;
using MediConnectBackend.Dtos.Availability;
using MediConnectBackend.Dtos.TimeSlot;
using MediConnectBackend.Models;

namespace MediConnectBackend.Mappers
{
    public static class AvailabilityMapper
    {
        public static Availability ToModel(CreateAvailabilityRequestDto dto)
        {
            var availability = new Availability
            {
                DoctorId = dto.DoctorId,
                DayOfWeek = dto.DayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsRecurring = dto.IsRecurring,
            };

            return availability;
        }

        public static AvailabilityResponseDto ToDto(Availability availability)
        {
            return new AvailabilityResponseDto
            {
                Id = availability.Id,
                DoctorId = availability.DoctorId,
                DayOfWeek = availability.DayOfWeek,
                StartTime = availability.StartTime,
                EndTime = availability.EndTime,
                IsRecurring = availability.IsRecurring,
                TimeSlots = availability.TimeSlots != null
                    ? availability.TimeSlots.Select(TimeSlotMapper.ToDto).ToList()
                    : new List<TimeSlotResponseDto>()
            };
        }

    }
}
