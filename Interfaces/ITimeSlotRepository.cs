using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediConnectBackend.Models;

namespace MediConnectBackend.Interfaces
{
    public interface ITimeSlotRepository
    {
        Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsAsync(string doctorId, DateTime date);
        Task<TimeSlot> GetTimeSlotByIdAsync(int id);
        Task CreateTimeSlotAsync(TimeSlot timeSlot);
        Task UpdateTimeSlotsAsync(TimeSlot timeSlot);
        Task DeleteTimeSlotAsync(int id);
    }
}
