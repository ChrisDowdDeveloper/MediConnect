using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Dtos.Patient;
using MediConnectBackend.Models;

namespace MediConnectBackend.Interfaces
{
    public interface IPatientRepository
    {
        Task<PatientDto?> GetPatientByIdAsync(string patientId);
        Task<Patient?> UpdatePatientAsync(string patientId, UpdatePatientDto patientDto);
        Task<bool> DeletePatientAsync(string patientId);
        Task<bool> PatientExistsByEmailAsync(string email);
        Task<Patient?> GetPatientByEmailAsync(string email);
    }
}