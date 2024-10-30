using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediConnectBackend.Helpers;
using MediConnectBackend.Models;

namespace MediConnectBackend.Interfaces
{
    public interface ITimeSlotRepository
    {
        Task<IEnumerable<TimeSlot>> GetAllTimeSlotsByDoctorAsync(string doctorId);
        Task<IEnumerable<TimeSlot>> GetAllAvailableTimeSlotsByDoctorAsync(string doctorId);
        Task<TimeSlot> GetTimeSlotByIdAsync(int timeSlotId);
        Task<TimeSlot> CreateTimeSlotAsync(TimeSlot timeSlot);
        Task<TimeSlot> UpdateTimeSlotAsync(TimeSlot timeSlot);
        Task<bool> DeleteTimeSlotAsync(int id);
    }

}
