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
                TimeSlots = GenerateTimeSlots(dto.DayOfWeek, dto.StartTime, dto.EndTime, dto.DoctorId)
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

        public static void UpdateModel(Availability availability, UpdateAvailabilityDto dto)
        {
            availability.DayOfWeek = dto.DayOfWeek;
            availability.StartTime = dto.StartTime;
            availability.EndTime = dto.EndTime;
            availability.IsRecurring = dto.IsRecurring;

            // Remove existing TimeSlots
            availability.TimeSlots.Clear();

            // Generate new TimeSlots
            var newTimeSlots = GenerateTimeSlots(dto.DayOfWeek, dto.StartTime, dto.EndTime, availability.DoctorId);
            foreach (var timeSlot in newTimeSlots)
            {
                availability.TimeSlots.Add(timeSlot);
            }
        }

        private static List<TimeSlot> GenerateTimeSlots(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime, string doctorId)
        {
            var timeSlots = new List<TimeSlot>();
            var slotDuration = TimeSpan.FromMinutes(30); // Adjust as needed
            var currentTime = startTime;

            while (currentTime < endTime)
            {
                var startDateTime = GetNextDateForDay(dayOfWeek).Add(currentTime);
                var endDateTime = startDateTime.Add(slotDuration);

                timeSlots.Add(new TimeSlot
                {
                    DoctorId = doctorId,
                    StartDateTime = startDateTime,
                    EndDateTime = endDateTime,
                    IsBooked = false
                });

                currentTime = currentTime.Add(slotDuration);
            }

            return timeSlots;
        }

        private static DateTime GetNextDateForDay(DayOfWeek day)
        {
            var today = DateTime.Today;
            int daysToAdd = ((int)day - (int)today.DayOfWeek + 7) % 7;
            if (daysToAdd == 0)
                daysToAdd = 7; // Next week

            return today.AddDays(daysToAdd);
        }
    }
}
