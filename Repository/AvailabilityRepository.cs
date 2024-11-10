using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediConnectBackend.Data;
using MediConnectBackend.Dtos.Availability;
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

        public async Task<Availability> CreateAsync(Availability availability)
        {
            _context.Availabilities.Add(availability);
            await _context.SaveChangesAsync();
            return availability;
        }

        public async Task<Availability?> DeleteAsync(int id)
        {
            var availability = await _context.Availabilities.FirstOrDefaultAsync(a => a.Id == id);
            if(availability == null)
            {
                return null;
            }
            _context.Availabilities.Remove(availability);
            await _context.SaveChangesAsync();
            return availability;
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

        public async Task<Availability?> UpdateAsync(int id, UpdateAvailabilityDto availabilityDto)
        {
            var availability = await _context.Availabilities.FirstOrDefaultAsync(a => a.Id == id);
            if(availability == null)
            {
                return null;
            }
            availability.DoctorId = availabilityDto.DoctorId;
            availability.DayOfWeek = availabilityDto.DayOfWeek;
            availability.StartTime = availabilityDto.StartTime;
            availability.EndTime = availabilityDto.EndTime;
            availability.IsRecurring = availabilityDto.IsRecurring;
            availability.TimeSlots = availabilityDto.TimeSlots
                .Select(availabilityDto => new TimeSlot
                {
                    AvailabilityId = availabilityDto.Id,
                    StartDateTime = availabilityDto.StartDateTime,
                    EndDateTime = availabilityDto.EndDateTime,
                    IsBooked = availabilityDto.IsBooked
                }).ToList();

            await _context.SaveChangesAsync();
            return availability;
        }
    }
}
