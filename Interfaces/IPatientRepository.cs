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
        Task<Patient?> GetPatientByIdAsync(string patientId);
        Task<Patient?> UpdatePatientAsync(Patient patient);
        Task<bool> DeletePatientAsync(string patientId);
        Task<bool> PatientExistsByEmailAsync(string email);
        Task<Patient?> GetPatientByEmailAsync(string email);
    }
}