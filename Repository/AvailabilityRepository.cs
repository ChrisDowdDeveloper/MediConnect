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
            var availability = await _context.Availabilities
                .Include(a => a.TimeSlots)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (availability == null) return false;

            // EF Core will handle deleting TimeSlots due to cascade delete
            _context.Availabilities.Remove(availability);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Availability>> GetAllAvailabilityByDoctorAsync(string doctorId)
        {
            return await _context.Availabilities
                .Include(a => a.TimeSlots)
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<Availability> GetAvailabilityByIdAsync(int id)
        {
            var availability = await _context.Availabilities
                .Include(a => a.TimeSlots)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (availability == null)
                throw new KeyNotFoundException("No availability found with that id");

            return availability;
        }

        public async Task<IEnumerable<Availability>> GetRecurringAvailabilityByDoctorAsync(string doctorId)
        {
            return await _context.Availabilities
                .Include(a => a.TimeSlots)
                .Where(a => a.DoctorId == doctorId && a.IsRecurring)
                .ToListAsync();
        }

        public async Task<Availability> UpdateAvailabilityAsync(Availability availability)
        {
            // Remove existing TimeSlots
            var existingTimeSlots = _context.TimeSlots.Where(ts => ts.AvailabilityId == availability.Id);
            _context.TimeSlots.RemoveRange(existingTimeSlots);

            // Update the Availability
            _context.Availabilities.Update(availability);

            await _context.SaveChangesAsync();
            return availability;
        }
    }
}
