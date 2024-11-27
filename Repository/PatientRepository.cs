using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Data;
using MediConnectBackend.Dtos.Patient;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            var patient = await _context.Patients
                .Include(p => p.PastAppointments)
                .FirstOrDefaultAsync(p => p.Id == patientId);

            if (patient == null)
            {
                return false;
            }

            _context.PastAppointments.RemoveRange(patient.PastAppointments);

            _context.Patients.Remove(patient);

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<Patient?> GetPatientByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Patient"))
                throw new KeyNotFoundException($"No patient found with email: {email}");

            var patient = user as Patient ?? throw new InvalidOperationException("User is not a patient.");
            return patient;
        }

        public async Task<Patient?> GetPatientByIdAsync(string patientId)
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

        public async Task<Patient?> UpdatePatientAsync(string patientId, UpdatePatientDto patientDto)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patientId);
            if(patient == null)
            {
                return null;
            }
            patient.FirstName = patientDto.FirstName;
            patient.LastName = patientDto.LastName;
            patient.DateOfBirth = patientDto.DateOfBirth;
            patient.Gender = patientDto.Gender;
            patient.Address = patientDto.Address;
            patient.PhoneNumber = patientDto.PhoneNumber;
            patient.EmergencyContactFirstName = patientDto.EmergencyContactFirstName;
            patient.EmergencyContactLastName = patientDto.EmergencyContactLastName;
            patient.EmergencyContactPhoneNumber = patientDto.EmergencyContactPhoneNumber;
            
            await _context.SaveChangesAsync();
            return patient;
        }
    }

}