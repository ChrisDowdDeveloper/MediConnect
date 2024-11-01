using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Availability;
using MediConnectBackend.Models;

namespace MediConnectBackend.Mappers
{
    public class AvailabilityMapper
    {
        public static Availability ToModel(CreateAvailabilityRequestDto dto)
        {
            return new Availability
            {
                DoctorId = dto.DoctorId,
                DayOfWeek = dto.DayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsRecurring = dto.IsRecurring
            };
        }

        public static AvailabilityResponseDto ToDto(Availability availability)
        {
            if(string.IsNullOrEmpty(availability.DoctorId))
            {
                throw new InvalidOperationException("DoctorId cannot be null");
            }

            return new AvailabilityResponseDto
            {
                Id = availability.Id,
                DoctorId = availability.DoctorId,
                DayOfWeek = availability.DayOfWeek,
                StartTime = availability.StartTime,
                EndTime = availability.EndTime,
                IsRecurring = availability.IsRecurring
            };
        }

        public static void UpdateModel(Availability availability, UpdateAvailabilityDto dto)
        {
            availability.Id = dto.Id;
            availability.DayOfWeek = dto.DayOfWeek ?? availability.DayOfWeek;
            availability.StartTime = dto.StartTime ?? availability.StartTime;
            availability.EndTime = dto.EndTime ?? availability.EndTime;
            availability.IsRecurring = dto.IsRecurring ?? availability.IsRecurring;

        }
    }
}