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

        public async Task<PatientDto?> GetPatientByIdAsync(string patientId)
        {
            var user = await _userManager.FindByIdAsync(patientId);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Patient"))
                throw new KeyNotFoundException("No patient found.");

            var patientDto = await _context.Patients
                .Include(p => p.Appointments)
                    .ThenInclude(a => a.Doctor)
                .Include(p => p.PastAppointments)
                    .ThenInclude(pa => pa.Doctor)
                .Select(p => new PatientDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Appointments = p.Appointments.Select(a => new AppointmentDto
                    {
                        Id = a.Id,
                        PatientId = a.PatientId,
                        DoctorId = a.DoctorId,
                        Doctor = a.Doctor != null ? new DoctorAppointmentResponseDto
                        {
                            Id = a.Doctor.Id,
                            FirstName = a.Doctor.FirstName,
                            LastName = a.Doctor.LastName,
                            PhoneNumber = a.Doctor.PhoneNumber,
                            Specialty = a.Doctor.Specialty,
                            YearsOfExperience = a.Doctor.YearsOfExperience,
                            OfficeAddress = a.Doctor.OfficeAddress
                        } : null,
                        AppointmentDateTime = a.AppointmentDateTime,
                        AppointmentStatus = a.AppointmentStatus.ToString(),
                        Notes = a.Notes,
                        CreationDate = a.CreationDate,
                        LastUpdatedDate = a.LastUpdatedDate
                    }).ToList(),
                    PastAppointments = p.PastAppointments.Select(pa => new PastAppointmentDto
                    {
                        Id = pa.Id,
                        AppointmentDateTime = pa.AppointmentDateTime,
                        Doctor = pa.Doctor != null ? new DoctorAppointmentResponseDto
                        {
                            Id = pa.Doctor.Id,
                            FirstName = pa.Doctor.FirstName,
                            LastName = pa.Doctor.LastName,
                            PhoneNumber = pa.Doctor.PhoneNumber,
                            Specialty = pa.Doctor.Specialty,
                            YearsOfExperience = pa.Doctor.YearsOfExperience,
                            OfficeAddress = pa.Doctor.OfficeAddress
                        } : null
                    }).ToList()
                })
                .FirstOrDefaultAsync(p => p.Id == user.Id);

            return patientDto;
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