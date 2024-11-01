using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Data;
using MediConnectBackend.Interfaces;
using MediConnectBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace MediConnectBackend.Repository
{
    public class AvailabilityRepository : IAvailabilityRepository
    {

        private readonly ApplicationDBContext _context;

        public AvailabilityRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Availability> CreateAvailabilityAsync(Availability availability)
        {
            _context.Availabilities.Add(availability);
            await _context.SaveChangesAsync();
            return availability;
        }

        public async Task<bool> DeleteAvailabilityAsync(int id)
        {
            var availability = await _context.Availabilities.FindAsync(id);
            if(availability == null) return false;
            _context.Availabilities.Remove(availability);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Availability>> GetAllAvailabilityByDoctorAsync(string doctorId)
        {
            return await _context.Availabilities.Where(a => a.DoctorId == doctorId).ToListAsync();
        }

        public async Task<Availability> GetAvailabilityByIdAsync(int id)
        {
            var availability = await _context.Availabilities.FirstOrDefaultAsync(a => a.Id == id) ?? throw new KeyNotFoundException("No availabiility found with that id");
            return availability;
        }


        public async Task<IEnumerable<Availability>> GetRecurringAvailabilityByDoctorAsync(string doctorId)
        {
            return await _context.Availabilities.Where(a => a.DoctorId == doctorId && a.IsRecurring).ToListAsync();
        }

        public async Task<Availability> UpdateAvailabilityAsync(Availability availability)
        {
            _context.Availabilities.Update(availability);
            await _context.SaveChangesAsync();
            return availability;
        }
    }
}