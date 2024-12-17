using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Data;
using MediConnectBackend.Dtos.Appointment;
using MediConnectBackend.Dtos.Doctor;
using MediConnectBackend.Dtos.PastAppointment;
using MediConnectBackend.Dtos.Patient;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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
            if(user == null || !await _userManager.IsInRoleAsync(user, "Patient"))
            {
                throw new KeyNotFoundException($"No patient found with email: {email}");
            }

            var patient = await _context.Patients
                .Include(p => p.Appointments)
                .Include(p => p.PastAppointments)
                .FirstOrDefaultAsync(p => p.Id == user.Id) ?? throw new KeyNotFoundException($"No patient record found for user with email: {email}");
            
            return patient;
        }

        public async Task<Patient?> GetPatientByIdAsync(string patientId)
        {
            var user = await _userManager.FindByIdAsync(patientId);
            if(user == null || !await _userManager.IsInRoleAsync(user, "Patient"))
                throw new KeyNotFoundException("No patient found.");
            
            var patient = await _context.Patients
                    .Include(p => p.Appointments)
                        .ThenInclude(a => a.Doctor)
                    .Include(p => p.PastAppointments)
                        .ThenInclude(pa => pa.Doctor)
                    .FirstOrDefaultAsync(p => p.Id == user.Id);
                    
            return patient;
        }

        public async Task<bool> PatientExistsByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null && await _userManager.IsInRoleAsync(user, "Patient");
        }

        public async Task<Patient?> UpdatePatientAsync(Patient patient)
        {
            var result = await _userManager.UpdateAsync(patient);
            if(result.Succeeded) return patient;
            throw new InvalidOperationException("Failed to update patient");
        }

    }
}