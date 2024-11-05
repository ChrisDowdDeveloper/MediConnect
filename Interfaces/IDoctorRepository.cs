using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Helpers;
using MediConnectBackend.Models;

namespace MediConnectBackend.Interfaces
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAllDoctorsAsync(DoctorQueryObject query);
        Task<Doctor> GetDoctorByIdAsync(string doctorId);
        Task<Doctor> UpdateDoctorAsync(Doctor doctor);
        Task<bool> DeleteDoctorAsync(string doctorId);
        Task UpdateDoctorWithAvailabilitiesAsync(Doctor doctor, List<Availability> newAvailabilities);

    }
}