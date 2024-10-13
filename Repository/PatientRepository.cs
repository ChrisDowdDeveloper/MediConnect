using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Data;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Models;
using Microsoft.AspNetCore.Identity;

namespace MediConnectBackend.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _userManager;

        public PatientRepository(ApplicationDBContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> DeletePatientAsync(string patientId)
        {
            var patient = await _userManager.FindByIdAsync(patientId);

            if (patient == null || !await _userManager.IsInRoleAsync(patient, "Patient"))
                return false;

            var result = await _userManager.DeleteAsync(patient);
            return result.Succeeded;
        }

        public async Task<Patient> GetPatientByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Patient"))
                throw new KeyNotFoundException($"No patient found with email: {email}");

            var patient = user as Patient ?? throw new InvalidOperationException("User is not a patient.");
            return patient;
        }

        public async Task<Patient> GetPatientByIdAsync(string patientId)
        {
            var user = await _userManager.FindByIdAsync(patientId);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Patient"))
                throw new KeyNotFoundException("No patient found.");

            var patient = user as Patient ?? throw new InvalidOperationException("User is not a patient.");
            return patient;
        }

        public async Task<bool> PatientExistsByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null && await _userManager.IsInRoleAsync(user, "Patient");
        }

        public async Task<Patient> UpdatePatientAsync(Patient patient)
        {
            var result = await _userManager.UpdateAsync(patient);
            if (result.Succeeded)
                return patient;

            throw new InvalidOperationException("Failed to update patient");
        }
    }

}