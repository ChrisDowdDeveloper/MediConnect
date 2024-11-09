using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Availability;
using MediConnectBackend.Dtos.Doctor;
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
        public static Availability ToModel(UpdateAvailabilityDto dto)
        {
            return new Availability
            {
                Id = dto.Id,
                DayOfWeek = dto.DayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsRecurring = dto.IsRecurring
            };
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
                IsRecurring = availability.IsRecurring
            };
        }

        public static void UpdateModel(Availability availability, UpdateAvailabilityDto dto)
        {
            availability.DoctorId = dto.DoctorId;
            availability.DayOfWeek = dto.DayOfWeek;
            availability.StartTime = dto.StartTime;
            availability.EndTime = dto.EndTime;
            availability.IsRecurring = dto.IsRecurring;
        }
    }
}