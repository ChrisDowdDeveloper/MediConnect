using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Data;
using MediConnectBackend.Helpers;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MediConnectBackend.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _userManager;

        public DoctorRepository(ApplicationDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> DeleteDoctorAsync(string doctorId)
        {
            var doctor = await _userManager.FindByIdAsync(doctorId);
            if (doctor == null || !await _userManager.IsInRoleAsync(doctor, "Doctor"))
                return false;

            var result = await _userManager.DeleteAsync(doctor);
            return result.Succeeded;
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync(DoctorQueryObject query)
        {
            var doctorsQuery = _context.Doctors.AsQueryable();

            if (!string.IsNullOrEmpty(query.FirstName))
            {
                doctorsQuery = doctorsQuery.Where(d => d.FirstName.Contains(query.FirstName));
            }

            if (!string.IsNullOrEmpty(query.LastName))
            {
                doctorsQuery = doctorsQuery.Where(d => d.LastName.Contains(query.LastName));
            }

            if (!string.IsNullOrEmpty(query.Specialty))
            {
                doctorsQuery = doctorsQuery.Where(d => d.Specialty.Contains(query.Specialty));
            }

            if (!string.IsNullOrEmpty(query.OfficeAddress))
            {
                doctorsQuery = doctorsQuery.Where(d => d.OfficeAddress.Contains(query.OfficeAddress));
            }

            if (query.YearsOfExperience > 0)
            {
                doctorsQuery = doctorsQuery.Where(d => d.YearsOfExperience >= query.YearsOfExperience);
            }

            if (query.IsDescending)
            {
                doctorsQuery = doctorsQuery.OrderByDescending(d => d.YearsOfExperience);
            }
            else
            {
                doctorsQuery = doctorsQuery.OrderBy(d => d.YearsOfExperience);
            }

            var skip = (query.PageNumber - 1) * query.PageSize;
            doctorsQuery = doctorsQuery.Skip(skip).Take(query.PageSize);

            return await doctorsQuery.ToListAsync();
        }

        public async Task<Doctor> GetDoctorByIdAsync(string doctorId)
        {
            var user = await _userManager.FindByIdAsync(doctorId);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Doctor"))
                throw new KeyNotFoundException("No doctor found.");

            return user as Doctor ?? throw new InvalidOperationException("User is not a doctor.");
        }

        public async Task<Doctor> UpdateDoctorAsync(Doctor doctor)
        {
            if (!await _userManager.IsInRoleAsync(doctor, "Doctor"))
                throw new InvalidOperationException("User is not a doctor.");

            var result = await _userManager.UpdateAsync(doctor);
            if (result.Succeeded) 
                return doctor;

            throw new InvalidOperationException("Failed to update doctor");
        }
    }
}
