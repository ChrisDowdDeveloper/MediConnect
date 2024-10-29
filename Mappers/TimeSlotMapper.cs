using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.TimeSlot;
using MediConnectBackend.Models;

namespace MediConnectBackend.Mappers
{
    // Mappers/TimeSlotMapper.cs
public static class TimeSlotMapper
{
    public static TimeSlotResponseDto ToResponseDto(TimeSlot timeSlot)
    {
        return new TimeSlotResponseDto
        {
            Id = timeSlot.Id,
            DoctorId = timeSlot.DoctorId,
            StartDateTime = timeSlot.StartDateTime,
            EndDateTime = timeSlot.EndDateTime,
            IsBooked = timeSlot.IsBooked,
        };
    }
    
    public static TimeSlot ToModel(CreateTimeSlotRequestDto dto)
    {
        return new TimeSlot
        {
            DoctorId = dto.DoctorId,
            StartDateTime = dto.StartDateTime,
            EndDateTime = dto.EndDateTime,
            IsBooked = false,
        };
    }
    
    public static void UpdateModel(TimeSlot timeSlot, UpdateTimeSlotDto dto)
    {
        if (dto.StartDateTime.HasValue)
            timeSlot.StartDateTime = dto.StartDateTime.Value;
        
        if (dto.EndDateTime.HasValue)
            timeSlot.EndDateTime = dto.EndDateTime.Value;
        
        if (dto.IsBooked.HasValue)
            timeSlot.IsBooked = dto.IsBooked.Value;
    }
    }

}