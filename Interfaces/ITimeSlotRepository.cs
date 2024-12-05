using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.TimeSlot;
using MediConnectBackend.Helpers;
using MediConnectBackend.Models;

namespace MediConnectBackend.Interfaces
{
    public interface ITimeSlotRepository
    {
        Task<IEnumerable<TimeSlot>> GetAllTimeSlotsByDoctorAsync(string doctorId);
        Task<IEnumerable<TimeSlot>> GetAllAvailableTimeSlotsByDoctorAsync(string doctorId);
        Task<TimeSlot> GetTimeSlotByIdAsync(int timeSlotId);
        Task<IEnumerable<TimeSlot>> GetTimeSlotsByAvailabilityId(int availabilityId);
        Task<TimeSlot> CreateTimeSlotAsync(TimeSlot timeSlot);
        Task<TimeSlot?> UpdateTimeSlotAsync(int id, UpdateTimeSlotDto dto);
        Task<bool> DeleteTimeSlotAsync(int id);
    }

}
