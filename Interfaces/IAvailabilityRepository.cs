using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Availability;
using MediConnectBackend.Models;

namespace MediConnectBackend.Interfaces
{
    public interface IAvailabilityRepository
    {
        Task<IEnumerable<Availability>> GetAllAvailabilityByDoctorAsync(string doctorId);
        Task<IEnumerable<Availability>> GetRecurringAvailabilityByDoctorAsync(string doctorId);
        Task<Availability> GetAvailabilityByIdAsync(int id);
        Task<Availability> CreateAsync(Availability availability);
        Task<Availability?> UpdateAsync(int id, UpdateAvailabilityDto availabilityDto);
        Task<Availability?> DeleteAsync(int id);
    }
}