using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Models;

namespace MediConnectBackend.Interfaces
{
    public interface IAvailabilityRepository
    {
        Task<IEnumerable<Availability>> GetAllAvailabilityByDoctorAsync(string doctorId);
        Task<IEnumerable<Availability>> GetRecurringAvailabilityByDoctorAsync(string doctorId);
        Task<Availability> GetAvailabilityByIdAsync(int id);
        Task<Availability> CreateAvailabilityAsync(Availability availability);
        Task<Availability> UpdateAvailabilityAsync(Availability availability);
        Task<bool> DeleteAvailabilityAsync(int id);
    }
}